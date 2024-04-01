using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Aspect", menuName = "Aspect/Create New Aspect")]
public class Aspect : ScriptableObject
{
    public int id;
    public string aspectName;
    public string category;
    public int value;
    public Sprite icon;
}
