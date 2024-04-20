using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public enum MonsterName
{
    Cat = 100,
    Reptile = 200,
    Soul = 300,
    Krabster = 400
}

[CreateAssetMenu(fileName = "Monster", menuName = "ScriptableObjects/Data/Monster")]
public class MonsterDetails : ScriptableObject
{
    [Header("Information")]
    public MonsterName _name;
    [TextArea(5, 10)] public string description;


    [Header("Attack")] 
    public int damage;

    [Header("Tamed Behaviour")] 
    public BehaviourTree tamedTree;
}
