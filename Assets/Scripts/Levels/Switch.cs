using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour, IDamageable
{
    [SerializeField] private ProjectileData.ProjectileType ProjectileType;

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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            InformationPanel.Instance.ShowInformation("Look like you need some to hit it with something");
        }
    }
    
    

    public void TakeDamage(int damage, IDamageable.DamagerTarget damagerType, Vector2 attackDirection,
        ProjectileData.ProjectileType projectileType = ProjectileData.ProjectileType.None)
    {
        if (projectileType == this.ProjectileType)
        {
            OnActivate?.Invoke();
        }
    }
    
}
