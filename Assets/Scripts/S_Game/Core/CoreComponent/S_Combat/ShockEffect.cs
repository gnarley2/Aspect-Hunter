using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shock_Effect : MonoBehaviour
{
    public float DestroyTimer = 5f;
    public float shockDuration = 5f; // Duration of the poison effect in seconds
    public int damagePerSecond = 5; // Damage to apply every second
    public float damageInterval = 1f; // Interval between damage applications in seconds

    private float timer;
    private float destroyTimer;
    private IDamageable target;
    private int damage = 10;

    private Coroutine damageCoroutine;
    private Coroutine destroyCoroutine;

    private void Start()
    {
        timer = shockDuration;
        destroyTimer = DestroyTimer;
        destroyCoroutine = StartCoroutine(DestroyAfterDelay());
    }

    private void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<IDamageable>(out IDamageable newTarget))
        {
            target = newTarget;
            target.TakeDamage(damage, IDamageable.DamagerTarget.Player, Vector2.zero, AspectType.Shock);
            damageCoroutine = StartCoroutine(DamageOverTime());
        }
    }

    private IEnumerator DamageOverTime()
    {
        while (timer > 0f && target != null)
        {
            yield return new WaitForSeconds(damageInterval);
            timer -= damageInterval;
            if (target != null)
            {
                target.TakeDamage(damagePerSecond, IDamageable.DamagerTarget.Player, Vector2.zero, AspectType.Shock);
            }
        }

        target = null;
    }

    private IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(destroyTimer);
        Destroy(gameObject);
    }
}