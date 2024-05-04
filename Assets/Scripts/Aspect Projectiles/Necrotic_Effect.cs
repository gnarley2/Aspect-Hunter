using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Necrotic_Effect : MonoBehaviour
{

    public float DestroyTimer = 5f;
    public float poisonDuration = 5f; // Duration of the poison effect in seconds
    public int damagePerSecond = 5; // Damage to apply every second
    public float damageInterval = 1f; // Interval between damage applications in seconds
    private float timer;
    private float destroyTimer;
    private IDamageable target;
    private int damage = 1;
    private Transform targetTransform; // Reference to the target's transform
    private IDamageable targetToDamage;
    private Coroutine damageCoroutine;
    private Coroutine destroyCoroutine;

    private void Start()
    {
        timer = poisonDuration;
        destroyTimer = DestroyTimer;
        destroyCoroutine = StartCoroutine(DestroyAfterDelay());
     
    }

    private void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<IDamageable>(out IDamageable target))
        {
            target.TakeDamage(damage, IDamageable.DamagerTarget.Player, Vector2.zero);
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

        // Destroy the necrotic effect when the duration is over
        Destroy(gameObject);
    }

    

    private IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(destroyTimer);
        Destroy(gameObject);
    }


    public void Initialize(IDamageable target)
    {
        targetToDamage = target;
        timer = poisonDuration;
        damageCoroutine = StartCoroutine(DamageOverTime());
    }
}
