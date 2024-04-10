using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "Monster", menuName = "ScriptableObjects/Data/Monster")]
public class MonsterDetails : ScriptableObject
{
    [Header("Information")]
    public string name;
    [TextArea(5, 10)] public string description;


    [Header("Attack")] 
    public int damage;

    [Header("Tamed Behaviour")] 
    public BehaviourTree tamedTree;
}
