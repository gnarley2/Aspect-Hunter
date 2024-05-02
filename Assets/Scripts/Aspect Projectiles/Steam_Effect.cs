using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steam_Effect : MonoBehaviour
{
    public float DestroyTimer = 5f;
    public float poisonDuration = 5f;
    public int damagePerSecond = 5;
    public float damageInterval = 1f;

    private float timer;
    private IDamageable targetToDamage;
    private Coroutine damageCoroutine;

    private void Start()
    {
        timer = poisonDuration;
        StartCoroutine(DestroyAfterDelay());

        // Get the parent's IDamageable component
        targetToDamage = GetComponentInParent<IDamageable>();

        // Start the damage over time coroutine
        if (targetToDamage != null)
        {
            damageCoroutine = StartCoroutine(DamageOverTime());
        }
    }

    private IEnumerator DamageOverTime()
    {
        while (timer > 0f)
        {
            yield return new WaitForSeconds(damageInterval);
            timer -= damageInterval;
            if (targetToDamage != null)
            {
                targetToDamage.TakeDamage(damagePerSecond, IDamageable.DamagerTarget.Player, Vector2.zero);
            }
        }

        Destroy(gameObject);
    }

    private IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(DestroyTimer);
        Destroy(gameObject);
    }
}
