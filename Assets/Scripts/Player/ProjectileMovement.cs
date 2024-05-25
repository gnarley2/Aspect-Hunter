using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    [SerializeField] private float speed = 0f; // Speed of the projectile
    private Vector3 direction;
    private Vector3 initialPosition;
    public float maxDistance = 10f;
    
    [SerializeField] private GameObject hitVFX;

    private IDamageable.DamagerTarget currentTarget;
    private int damage = 0;

    void Start()
    {
        initialPosition = transform.position;
    }
    
    void Update()
    {
        // Move the projectile in the specified direction
        transform.Translate(direction * (speed * Time.deltaTime), Space.World);

        if (Vector3.Distance(initialPosition, transform.position) >= maxDistance)
        {
            Destroy(gameObject); // Destroy the projectile
        }
    }
    
    public void Initialize(Vector3 newDirection, IDamageable.DamagerTarget target, int damage, float speed)
    {
        SetDirection(newDirection);
        currentTarget = target;
        this.damage = damage;
        this.speed = speed;
    }

    // Set the direction of movement for the projectile
    void SetDirection(Vector3 newDirection)
    {
        direction = newDirection.normalized;
        // Optionally, rotate the projectile to face the direction
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<IDamageable>(out IDamageable target) && target.GetDamagerType() != currentTarget)
        {
            if (target.GetDamagerType() == currentTarget) return;
            if (target.GetDamagerType() == IDamageable.DamagerTarget.TamedMonster &&
                currentTarget == IDamageable.DamagerTarget.Player) return;
            if (target.GetDamagerType() == IDamageable.DamagerTarget.Player &&
                currentTarget == IDamageable.DamagerTarget.TamedMonster) return;
            
            target.TakeDamage(damage, currentTarget, Vector2.zero);
            Destroy();
        }

        if (other.CompareTag("Frost_Wall"))
        {
            Instantiate(hitVFX, transform.position, Quaternion.Euler(transform.forward));
            Destroy();
        }

        if (other.CompareTag("Environment"))
        {
            Instantiate(hitVFX, transform.position, Quaternion.Euler(transform.forward));
            Destroy();
        }
    }

    private void Destroy()
    {
        StartCoroutine(DestroyCoroutine());
    }

    IEnumerator DestroyCoroutine()
    {
        if (hitVFX != null) Instantiate(hitVFX, transform.position, Quaternion.Euler(transform.forward));
        yield return null;
        
        Destroy(gameObject);
    }
}
