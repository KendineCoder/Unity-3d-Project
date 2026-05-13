using UnityEngine;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif

[RequireComponent(typeof(LineRenderer))]
public class RuneRecorder : MonoBehaviour
{
    [Header("Recording Setup")]
    public Transform wandTransform;
    [Tooltip("Check this to draw with your mouse instead of the 3D wand.")]
    public bool useMouseInsteadOfWand = true;
    public string runeNameToSave = "NewRune";
    public KeyCode recordKey = KeyCode.Space;

    [Header("Debug Visuals")]
    public bool showDebugVisuals = true;
    public Color trailColor = Color.cyan;

    private List<Vector2> recordedPoints = new List<Vector2>();
    private bool isRecording = false;
    private LineRenderer lineRenderer;

    void Start()
    {
        // Setup the Line Renderer for the trail
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.positionCount = 0;
        lineRenderer.useWorldSpace = true;

        // Give it a basic visible material if it doesn't have one
        if (lineRenderer.sharedMaterial == null)
        {
            lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
            lineRenderer.startColor = trailColor;
            lineRenderer.endColor = trailColor;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(recordKey))
        {
            Debug.Log("Recording Started... Move your mouse/wand!");
            recordedPoints.Clear();
            lineRenderer.positionCount = 0;
            isRecording = true;
        }

        if (Input.GetKey(recordKey) && isRecording)
        {
            Vector2 screenPos = Vector2.zero;

            if (useMouseInsteadOfWand)
            {
                screenPos = Input.mousePosition;
            }
            else if (wandTransform != null && Camera.main != null)
            {
                screenPos = Camera.main.WorldToScreenPoint(wandTransform.position);
            }

            // Only add point if we moved at least 2 pixels
            if (recordedPoints.Count == 0 || Vector2.Distance(recordedPoints[recordedPoints.Count - 1], screenPos) > 2f)
            {
                recordedPoints.Add(screenPos);
                UpdateTrail();
            }
        }

        if (Input.GetKeyUp(recordKey) && isRecording)
        {
            isRecording = false;
            SaveRune();
        }
    }

    private void UpdateTrail()
    {
        if (!showDebugVisuals || Camera.main == null) return;

        lineRenderer.positionCount = recordedPoints.Count;
        for (int i = 0; i < recordedPoints.Count; i++)
        {
            // Convert the 2D screen point into a 3D world point just in front of the camera
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(
                recordedPoints[i].x,
                recordedPoints[i].y,
                Camera.main.nearClipPlane + 1f // 1 unit in front of the camera
            ));
            lineRenderer.SetPosition(i, worldPos);
        }
    }

    // Draws the raw captured points directly to your game screen
    void OnGUI()
    {
        if (!showDebugVisuals || recordedPoints.Count == 0) return;

        GUI.color = Color.green;
        foreach (var p in recordedPoints)
        {
            // GUI coordinates are inverted on the Y axis compared to Screen coordinates
            Rect rect = new Rect(p.x - 3, Screen.height - p.y - 3, 6, 6);
            GUI.DrawTexture(rect, Texture2D.whiteTexture);
        }
    }

    private void SaveRune()
    {
        if (recordedPoints.Count < 10)
        {
            Debug.LogWarning($"Gesture too short! Only captured {recordedPoints.Count} points. You need at least 10.");
            return;
        }

#if UNITY_EDITOR
        RuneTemplate newRune = ScriptableObject.CreateInstance<RuneTemplate>();
        newRune.runeName = runeNameToSave;
        newRune.points = new List<Vector2>(recordedPoints);

        string folderPath = "Assets/Runes";
        if (!AssetDatabase.IsValidFolder(folderPath))
        {
            AssetDatabase.CreateFolder("Assets", "Runes");
        }

        string fullPath = $"{folderPath}/{runeNameToSave}.asset";
        AssetDatabase.CreateAsset(newRune, fullPath);
        AssetDatabase.SaveAssets();

        Debug.Log($"<color=cyan>Success! Saved '{runeNameToSave}' with {recordedPoints.Count} points.</color>");
#else
        Debug.LogError("Rune recording only works in the Unity Editor!");
#endif
    }
}