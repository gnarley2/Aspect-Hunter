using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCannon : MonoBehaviour
{
    [SerializeField] private Vector2 box;
    [SerializeField] private Vector2 offset;
    [SerializeField] private int damage;
    
    void DealDamage()
    {
        Collider2D[] cols = Physics2D.OverlapBoxAll(transform.position + (Vector3)offset, box, 0);
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
        Gizmos.DrawWireCube(transform.position + (Vector3)offset, box);
    }

    void Destroy()
    {
        Destroy(gameObject);
    }
}
