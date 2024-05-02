using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Switch : MonoBehaviour, IDamageable
{
    [FormerlySerializedAs("ProjectileType")] [SerializeField] private AspectType aspectType;

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
        AspectType aspectType = AspectType.None)
    {
        if (aspectType == this.aspectType)
        {
            OnActivate?.Invoke();
        }
    }
    
}
