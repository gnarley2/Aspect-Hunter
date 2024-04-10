using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Data/EnemyData", fileName = "Enemy Data")]
public class EnemyData : ScriptableObject
{
    public string ID;
    public MonsterData monsterData;
    
    [Header("Changed Component")]
    public HealthData healthData;
    public int currentHealth;

    public IDamageable.KnockbackType KnockbackType = IDamageable.KnockbackType.none;

    public EnemyData Clone()
    {
        EnemyData newData = Instantiate(this);
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
