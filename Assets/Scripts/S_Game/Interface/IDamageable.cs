using System;
using UnityEngine;

public interface IDamageable
{
    public enum DamagerTarget
    {
        Player,
        Enemy,
        TamedMonster,
        Trap
    }

    public enum KnockbackType
    {
        none,
        weak,
        strong,
        player
    }

    public Health GetHealth();
    public DamagerTarget GetDamagerType();
    public KnockbackType GetKnockbackType();
    public Vector2 GetPosition();
    public void TakeDamage(int damage, DamagerTarget damagerType, Vector2 attackDirection);
}
