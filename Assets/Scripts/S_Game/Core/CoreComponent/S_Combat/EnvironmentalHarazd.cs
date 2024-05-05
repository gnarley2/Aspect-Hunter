using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EnvironmentalHarazd : MonoBehaviour
{
    public float destroyTimer = 5f;
    public float damageDuration = 5f; // Duration of the poison effect in seconds
    public int damagePerInterval = 5; // Damage to apply every second
    public float damageInterval = 1f; // Interval between damage applications in seconds

    private IDamageable target;

    private Coroutine damageCoroutine;

    private void Start()
    {
        Destroy(gameObject, destroyTimer);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<IDamageable>(out IDamageable newTarget))
        {
            if (newTarget.GetDamagerType() == IDamageable.DamagerTarget.Enemy) return;
            
            StartCoroutine(DamageOverTime(newTarget));
        }
    }

    private IEnumerator DamageOverTime(IDamageable target)
    {
        if (target == null) yield break;
        
        float startTime = Time.time;
        while (startTime + damageDuration > Time.time)
        {
            target.TakeDamage(damagePerInterval, IDamageable.DamagerTarget.Enemy, Vector2.zero);
            yield return new WaitForSeconds(damageInterval);
        }
    }
}