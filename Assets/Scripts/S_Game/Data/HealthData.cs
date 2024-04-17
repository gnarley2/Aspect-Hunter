using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


[CreateAssetMenu(fileName = " Health", menuName = "ScriptableObjects/Data/HealthData")]
public class HealthData : ScriptableObject
{
    public int maxHealth;
}
