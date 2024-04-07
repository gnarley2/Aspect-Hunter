using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "Monster", menuName = "ScriptableObjects/Data/Monster")]
public class MonsterData : ScriptableObject
{
    public string ID;

    [Header("Information")]
    public string name;
    [TextArea(5, 10)] public string description;


    [Header("Attack")] 
    public int damage;

    [ContextMenu("Create ID")]
    public void CreateID()
    {
        if (!string.IsNullOrEmpty(ID))
        {
            Debug.LogError("Already have an ID");
            return;
        }
        
        ID = Guid.NewGuid().ToString();
    }
}
