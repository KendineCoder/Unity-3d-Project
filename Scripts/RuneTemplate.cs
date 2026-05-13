using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewRune", menuName = "Magic/Rune Template")]
public class RuneTemplate : ScriptableObject
{
    public string runeName;
    public List<Vector2> points = new List<Vector2>();
    
}