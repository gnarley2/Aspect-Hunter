using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonOrb : MonoBehaviour
{
    [SerializeField] private float radius;
    [SerializeField] private int damage;
    
    void DealDamage()
    {
        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, radius);
        foreach (Collider2D col in cols)
        {
            if (col.TryGetComponent<IDamageable>(out IDamageable target))
            {
                if (target.GetDamagerType() == IDamageable.DamagerTarget.Enemy) return;

                target.TakeDamage(damage, IDamageable.DamagerTarget.Enemy, Vector2.zero);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    void Destroy()
    {
        Destroy(gameObject);
    }
}
