using UnityEngine;
using System.Collections.Generic;

public static class RuneRecognizer
{
    public const int ResampleCount = 64;

    // Takes raw drawn points and turns them into a comparable 64-point normalized shape
    public static List<Vector2> ProcessPoints(List<Vector2> points)
    {
        if (points == null || points.Count < 2) return points;

        List<Vector2> resampled = Resample(points, ResampleCount);
        return Normalize(resampled);
    }

    private static List<Vector2> Resample(List<Vector2> points, int n)
    {
        float interval = PathLength(points) / (n - 1);
        float totalDist = 0;
        List<Vector2> newPoints = new List<Vector2> { points[0] };

        for (int i = 1; i < points.Count; i++)
        {
            float d = Vector2.Distance(points[i - 1], points[i]);
            if ((totalDist + d) >= interval)
            {
                float q = (interval - totalDist) / d;
                Vector2 resampledPoint = Vector2.Lerp(points[i - 1], points[i], q);
                newPoints.Add(resampledPoint);
                points.Insert(i, resampledPoint);
                totalDist = 0;
            }
            else
            {
                totalDist += d;
            }
        }

        // Catch rounding errors
        while (newPoints.Count < n) newPoints.Add(points[points.Count - 1]);
        return newPoints;
    }

    private static List<Vector2> Normalize(List<Vector2> points)
    {
        float xMin = float.MaxValue, xMax = float.MinValue;
        float yMin = float.MaxValue, yMax = float.MinValue;

        foreach (var p in points)
        {
            xMin = Mathf.Min(xMin, p.x); xMax = Mathf.Max(xMax, p.x);
            yMin = Mathf.Min(yMin, p.y); yMax = Mathf.Max(yMax, p.y);
        }

        float size = Mathf.Max(xMax - xMin, yMax - yMin);
        if (size < 0.001f) size = 1f; // Prevent NaN division by zero

        List<Vector2> normalized = new List<Vector2>();
        foreach (var p in points)
        {
            // ADD TAKING AVARAGE FOR CREATING HIT POINT
            // Centers and scales the points to fit inside a 1x1 box
            normalized.Add(new Vector2((p.x - xMin) / size, (p.y - yMin) / size));
        }
        return normalized;
    }

    private static float PathLength(List<Vector2> points)
    {
        float d = 0;
        for (int i = 1; i < points.Count; i++) d += Vector2.Distance(points[i - 1], points[i]);
        return d;
    }

    public static float GetDistance(List<Vector2> pathA, List<Vector2> pathB)
    {
        float distance = 0;
        int count = Mathf.Min(pathA.Count, pathB.Count);
        for (int i = 0; i < count; i++) distance += Vector2.Distance(pathA[i], pathB[i]);
        return distance / count;
    }
}