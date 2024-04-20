using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Data/ProjectileData")]
public class ProjectileData : ScriptableObject
{
    public enum ProjectileType
    {
        Fire,
        Frost,
        Shock,
        Poison,
        Water,
        None,
    }

    public string name;
    public ProjectileType type;
    public GameObject prefab;
    public bool isUnlocked = false;
}
