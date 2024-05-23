using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gas_Hit : MonoBehaviour
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
            damageCoroutine = StartCoroutine(DamageOverTime(target));

            // Attach the game object to the target
            targetTransform = other.transform;
            transform.parent = targetTransform;
        }
    }

    private IEnumerator DamageOverTime(IDamageable target)
    {
        while (timer > 0f)
        {
            yield return new WaitForSeconds(damageInterval);
            timer -= damageInterval;
            if (target != null)
            {
                target.TakeDamage(damagePerSecond, IDamageable.DamagerTarget.Player, Vector2.zero);
            }
        }
    }
    private IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(destroyTimer);
        Destroy(gameObject);
    }
}
