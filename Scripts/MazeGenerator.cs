using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;  
public class MazeGenerator : MonoBehaviour
{
    [Header("Maze Settings")]
    public int gridSize = 20;
    public float cellSize = 12f;
    public float wallScaleFactor = 1.5f;
    public float wallSpacing = 0.8f;
    public float wallHeight = 20f;

    [Header("Prefabs")]
    public GameObject wallPrefab;
    public GameObject battlementPrefab;

    private int[,,] maze; // [y, x, directions]
    private bool[,] visited;
 


void Start()
    {
        GenerateMaze();
    }

    void GenerateMaze()
    {
        maze = new int[gridSize, gridSize, 4]; // 0:North, 1:East, 2:South, 3:West
        for (int y = 0; y < gridSize; y++)
            for (int x = 0; x < gridSize; x++)
                for (int d = 0; d < 4; d++) maze[y, x, d] = 1;

        visited = new bool[gridSize, gridSize];
        Stack<Vector2Int> stack = new Stack<Vector2Int>();

        Vector2Int start = new Vector2Int(Random.Range(0, gridSize), Random.Range(0, gridSize));
        stack.Push(start);
        visited[start.y, start.x] = true;

        Vector2Int[] directions = { new Vector2Int(0, -1), new Vector2Int(1, 0), new Vector2Int(0, 1), new Vector2Int(-1, 0) };

        while (stack.Count > 0)
        {
            Vector2Int current = stack.Peek();
            List<int> neighbors = new List<int>();

            for (int i = 0; i < 4; i++)
            {
                Vector2Int next = current + directions[i];
                if (next.x >= 0 && next.x < gridSize && next.y >= 0 && next.y < gridSize && !visited[next.y, next.x])
                    neighbors.Add(i);
            }

            if (neighbors.Count > 0)
            {
                int dirIndex = neighbors[Random.Range(0, neighbors.Count)];
                Vector2Int next = current + directions[dirIndex];

                maze[current.y, current.x, dirIndex] = 0;
                maze[next.y, next.x, (dirIndex + 2) % 4] = 0; // Remove opposite wall

                visited[next.y, next.x] = true;
                stack.Push(next);
            }
            else
            {
                stack.Pop();
            }
        }

        // Entrance and Exit
        int entranceX = Random.Range(0, gridSize);
        maze[0, entranceX, 0] = 0;
        int exitX = Random.Range(0, gridSize);
        maze[gridSize - 1, exitX, 2] = 0;

        SpawnMazeObjects(entranceX, exitX);
    }

    void SpawnMazeObjects(int entranceX, int exitX)
    {
        float adjustedWallLength = cellSize * wallSpacing;
        float offset = (cellSize - adjustedWallLength) / 2;

        for (int y = 0; y < gridSize; y++)
        {
            for (int x = 0; x < gridSize; x++)
            {
                Vector3 pos = new Vector3(x * cellSize, wallHeight, y * cellSize);

                // North
                if (maze[y, x, 0] == 1)
                    InstantiateWall(pos + new Vector3(offset, 0, 0), 90, new Vector3(wallSpacing, 10, wallScaleFactor));
                // East
                if (maze[y, x, 1] == 1)
                    InstantiateWall(pos + new Vector3(cellSize, 0, offset), 0, new Vector3(wallSpacing, 10, wallScaleFactor));
                // South
                if (maze[y, x, 2] == 1)
                    InstantiateWall(pos + new Vector3(offset, 0, cellSize), 90, new Vector3(wallSpacing, 10, wallScaleFactor));
                // West
                if (maze[y, x, 3] == 1)
                    InstantiateWall(pos + new Vector3(0, 0, offset), 0, new Vector3(wallSpacing, 10, wallScaleFactor));
            }
        }
        //SpawnPerimeter(entranceX, exitX);
    }

    void InstantiateWall(Vector3 pos, float rotation, Vector3 scale)
    {
        GameObject wall = Instantiate(wallPrefab, pos, Quaternion.Euler(0, rotation, 0), transform);
        wall.transform.localScale = scale*2;
    }

    void SpawnPerimeter(int entX, int exX)
    {
        float bHeight = wallHeight + 2f;
        float spacing = cellSize * 1.2f;

        for (float i = 0; i <= gridSize * cellSize; i += spacing)
        {
            // North & South Perimeter (Simplified logic)
            if (Mathf.Abs(i - entX * cellSize) > cellSize / 2)
                Instantiate(battlementPrefab, new Vector3(i, bHeight, 0), Quaternion.Euler(0, 180, 0), transform);

            if (Mathf.Abs(i - exX * cellSize) > cellSize / 2)
                Instantiate(battlementPrefab, new Vector3(i, bHeight, gridSize * cellSize), Quaternion.identity, transform);

            // West & East Perimeter
            Instantiate(battlementPrefab, new Vector3(0, bHeight, i), Quaternion.Euler(0, 90, 0), transform);
            Instantiate(battlementPrefab, new Vector3(gridSize * cellSize, bHeight, i), Quaternion.Euler(0, 270, 0), transform);
        }
    }
    [ContextMenu("Step 1: Combine Mesh and Colliders")]
    public void CombineMazeMesh()
    {
        Quaternion oldRotation = transform.rotation;
        Vector3 oldPosition = transform.position;

        // Reset transform for accurate combining
        transform.rotation = Quaternion.identity;
        transform.position = Vector3.zero;

        MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();
        CombineInstance[] combine = new CombineInstance[meshFilters.Length];

        for (int i = 0; i < meshFilters.Length; i++)
        {
            // Skip the parent object itself if it already has a MeshFilter
            if (meshFilters[i].gameObject == gameObject) continue;

            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;

            // IMPORTANT: Disable individual colliders and objects 
            // to prevent physics engine overhead and "double" collisions.
            meshFilters[i].gameObject.SetActive(false);
        }

        // Create the new Mesh
        Mesh finalMesh = new Mesh();
        finalMesh.name = "CombinedMazeMesh";
        finalMesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
        finalMesh.CombineMeshes(combine);

        // Ensure components exist on the parent
        MeshFilter mf = gameObject.GetComponent<MeshFilter>();
        if (mf == null) mf = gameObject.AddComponent<MeshFilter>();

        MeshRenderer mr = gameObject.GetComponent<MeshRenderer>();
        if (mr == null) mr = gameObject.AddComponent<MeshRenderer>();

        MeshCollider mc = gameObject.GetComponent<MeshCollider>();
        if (mc == null) mc = gameObject.AddComponent<MeshCollider>();

        // Assign the combined mesh to Filter and Collider
        mf.mesh = finalMesh;
        mc.sharedMesh = null; // Reset first to force a refresh
        mc.sharedMesh = finalMesh;

        // Assign material from the first wall found
        if (meshFilters.Length > 0)
            mr.material = meshFilters[0].GetComponent<MeshRenderer>().sharedMaterial;

        // Restore original transform
        transform.position = oldPosition;
        transform.rotation = oldRotation;

        Debug.Log("Maze combined into one mesh with a single MeshCollider!");
    }
    [ContextMenu("Step 2: Save Asset")]
    [ContextMenu("Step 2: Save Asset")]
    public void SaveMazeAsAsset()
    {
        MeshFilter mf = GetComponent<MeshFilter>();
        MeshCollider mc = GetComponent<MeshCollider>();

        if (mf == null || mf.sharedMesh == null)
        {
            Debug.LogError("No combined mesh found! Run CombineMazeMesh first.");
            return;
        }

        // 1. Setup Folder
        string folderPath = "Assets/SavedMazes";
        if (!AssetDatabase.IsValidFolder(folderPath))
        {
            AssetDatabase.CreateFolder("Assets", "SavedMazes");
        }

        // 2. Define Path
        string fileName = $"Maze_{gridSize}x{gridSize}_{System.DateTime.Now:yyyyMMdd_HHmmss}.asset";
        string fullPath = Path.Combine(folderPath, fileName);

        // 3. Save the mesh asset
        // We create a copy or use the sharedMesh to ensure it's a project asset
        AssetDatabase.CreateAsset(mf.sharedMesh, fullPath);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        // 4. CRITICAL STEP: Re-assign the SAVED asset to the components
        // This links the scene object to the file on disk so the Prefab knows where to look
        Mesh savedMesh = AssetDatabase.LoadAssetAtPath<Mesh>(fullPath);
        mf.sharedMesh = savedMesh;

        if (mc != null)
        {
            mc.sharedMesh = savedMesh;
        }

        Debug.Log($"Maze asset saved and linked to Collider at: {fullPath}");
        EditorGUIUtility.PingObject(savedMesh);
    }
}