using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public enum MonsterName
{
    Cat = 100,
    Reptile = 200,
    Soul = 300,
    Krabster = 400,
    Iceboy = 500,
    FireRat = 600,
}

[CreateAssetMenu(fileName = "Monster", menuName = "ScriptableObjects/Data/Monster")]
public class MonsterDetails : ScriptableObject
{
    
    [Header("Information")]
    public MonsterName name;
    [TextArea(5, 10)] public string description;
    public AspectType type = AspectType.None;

    [Header("Attack")] 
    public int damage;

    [Header("Tamed Behaviour")] 
    public BehaviourTree tamedTree;

    [Header("Loot")] 
    public int maxNumAspect;
    public int minNumAspect;


    public int GetLoot()
    {
        return Random.Range(minNumAspect, maxNumAspect + 1);
    }
}
