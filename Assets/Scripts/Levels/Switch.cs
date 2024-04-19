using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour, IDamageable
{
    [SerializeField] private PlayerCombat.ProjectileType ProjectileType;

    public Action OnActivate;


    public Health GetHealth()
    {
        return null;
    }

    public IDamageable.DamagerTarget GetDamagerType()
    {
        return IDamageable.DamagerTarget.Trap;
    }

    public IDamageable.KnockbackType GetKnockbackType()
    {
        return IDamageable.KnockbackType.none;
    }

    public Vector2 GetPosition()
    {
        return transform.position;
    }

    public void TakeDamage(int damage, IDamageable.DamagerTarget damagerType, Vector2 attackDirection,
        PlayerCombat.ProjectileType projectileType = PlayerCombat.ProjectileType.None)
    {
        if (projectileType == this.ProjectileType)
        {
            OnActivate?.Invoke();
        }
    }
    
}
