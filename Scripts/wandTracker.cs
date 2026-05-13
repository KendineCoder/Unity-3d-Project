using UnityEngine;
using System.Collections.Generic;

public class WandTracker : MonoBehaviour
{

    public GameObject spellPrefab;
   
    
    [Header("References")]
    public List<RuneTemplate> runeTemplates;
    public TrailRenderer wandTrail;

    [Header("Settings")]
    public float matchThreshold = 0.2f; 
    public float spellSpawnDistance = 2.0f; 
    private List<Vector2> currentStroke = new List<Vector2>();
    private bool isDrawing = false;

    private Dictionary<string, List<Vector2>> processedTemplates = new Dictionary<string, List<Vector2>>();

    void Start()
    {
        if (wandTrail) wandTrail.emitting = false;

        foreach (var template in runeTemplates)
        {
            processedTemplates[template.runeName] = RuneRecognizer.ProcessPoints(template.points);
        }
    }

    void Update()
    {
       
        if (Input.GetMouseButtonDown(0)) StartDrawing();
        if (Input.GetMouseButtonUp(0)) StopDrawing();

        if (isDrawing)
        {
            Vector2 screenPos = Camera.main.WorldToScreenPoint(transform.position);

            
            if (currentStroke.Count == 0 || Vector2.Distance(currentStroke[currentStroke.Count - 1], screenPos) > 5f)
            {
                currentStroke.Add(screenPos);
            }
        }
    }

    private void StartDrawing()
    {
        isDrawing = true;
        currentStroke.Clear();
        if (wandTrail)
        {
            wandTrail.Clear();
            wandTrail.emitting = true;
        }
    }

    private void StopDrawing()
    {
        isDrawing = false;
        if (wandTrail) wandTrail.emitting = false;

        if (currentStroke.Count < 10)
        {
            Debug.Log("Drawing too short to recognize.");
            return;
        }

        RecognizeSpell();
    }

    private void RecognizeSpell()
    {
        List<Vector2> processedCandidate = RuneRecognizer.ProcessPoints(currentStroke);

        string bestMatch = "Unknown";
        
        float minScore = float.MaxValue;

        foreach (var kvp in processedTemplates)
        {
            float dist = RuneRecognizer.GetDistance(processedCandidate, kvp.Value);

            if (dist < minScore)
            {
                minScore = dist;
                bestMatch = kvp.Key;

            }
        }

        if (minScore <= matchThreshold)
        {
            Debug.Log($"<color=green>Spell Cast: {bestMatch} (Score: {minScore:F3})</color>");

            Vector2 screenCenter = GetStrokeCenter();

         
            Ray ray = Camera.main.ScreenPointToRay(screenCenter);

    
            Vector3 spawnPosition = ray.GetPoint(spellSpawnDistance);

           
            if (spellPrefab != null)
            {
                
                GameObject newSpell = Instantiate(spellPrefab, spawnPosition, Quaternion.LookRotation(ray.direction));

                SpellPrefab projectileScript = newSpell.AddComponent<SpellPrefab>();

                projectileScript.damage = 30f;
                projectileScript.speed = 15f;
            }
            else
            {
                Debug.LogWarning("Spell Prefab is not assigned in the WandTracker!");
            }
        }
        else
        {
            Debug.Log($"<color=red>Spell Fizzled! Closest was {bestMatch} but score was {minScore:F3}</color>");
        }
    }
    private Vector2 GetStrokeCenter()
    {
        if (currentStroke.Count == 0) return Vector2.zero;

        float minX = float.MaxValue, maxX = float.MinValue;
        float minY = float.MaxValue, maxY = float.MinValue;

        foreach (Vector2 p in currentStroke)
        {
            if (p.x < minX) minX = p.x;
            if (p.x > maxX) maxX = p.x;
            if (p.y < minY) minY = p.y;
            if (p.y > maxY) maxY = p.y;
        }

        return new Vector2((minX + maxX) / 2f, (minY + maxY) / 2f);
    }
    private void OnDisable()
    {
        // This runs automatically when wandTracker.gameObject.SetActive(false) is called.
        // It prevents the trail from freezing mid-air or remembering half a drawing.
        isDrawing = false;
        currentStroke.Clear();

        if (wandTrail != null)
        {
            wandTrail.emitting = false;
            wandTrail.Clear();
        }
    }
}