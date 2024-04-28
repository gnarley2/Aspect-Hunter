using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water_Splash : MonoBehaviour
{
    [SerializeField] private float destroyDuration = 2f; // Duration before destroying the game object
    private int damage = 10;
    public GameObject waterEffect;
    private void Start()
    {
        // Start a coroutine to destroy the game object after the specified duration
        StartCoroutine(DestroyAfterDuration());
    }
    public void Initialize(Vector3 newDirection, int damage)
    {
        // SetDirection(newDirection);
        this.damage = damage;
    }


    private System.Collections.IEnumerator DestroyAfterDuration()
    {
        // Wait for the specified duration
        yield return new WaitForSeconds(destroyDuration);
         Instantiate(waterEffect, transform.position, Quaternion.identity);
        // Destroy the game object
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<IDamageable>(out IDamageable target))
        {
            target.TakeDamage(damage, IDamageable.DamagerTarget.Player, Vector2.zero);
            Instantiate(waterEffect, transform.position, Quaternion.identity);
        }
    }
}
