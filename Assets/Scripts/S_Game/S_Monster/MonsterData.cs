using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


[CreateAssetMenu(menuName = "ScriptableObjects/Data/MonsterData", fileName = "Monster Data")]
public class MonsterData : ScriptableObject
{
    public string ID;
    public MonsterDetails monsterDetails;
    public bool isTamed;

    [Header("Changed Component")]
    public HealthData healthData;
    public int currentHealth;

    public IDamageable.KnockbackType KnockbackType = IDamageable.KnockbackType.none;

    public MonsterData Clone()
    {
        MonsterData newData = Instantiate(this);
        newData.CreateID();
        return newData;
    }

    [ContextMenu("Create ID")]
    public void CreateID()
    {
        ID = Guid.NewGuid().ToString();
    }
    
    public void UpdateCurrentHealth(int newHealth)
    {
        currentHealth = newHealth;
    }
}
