using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Data/EnemyData", fileName = "Enemy Data")]
public class EnemyData : ScriptableObject
{
    public MonsterData monsterData;
    
    [Header("Character Component")]
    public HealthData healthData;

    public IDamageable.KnockbackType KnockbackType = IDamageable.KnockbackType.weak;


}
