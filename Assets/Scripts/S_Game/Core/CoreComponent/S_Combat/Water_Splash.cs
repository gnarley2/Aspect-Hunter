using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water_Splash : MonoBehaviour
{
    [SerializeField] private float destroyDuration = 2f; // Duration before destroying the game object
    private int damage = 10;
    public GameObject waterEffect;
    private SpriteRenderer spriteRenderer; // Add this line
    private bool shouldFlipX;


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer component not found on " + gameObject.name);
        }
    }

    private void Start()
    {
        StartCoroutine(DestroyAfterDuration());
    }

    public void Initialize(Vector3 newDirection, int damage, Transform playerTransform)
    {
        this.damage = damage;
        Vector3 directionToPlayer = transform.position - playerTransform.position;
        shouldFlipX = directionToPlayer.x < 0;

        if (spriteRenderer != null) // Add a null check
        {
            spriteRenderer.flipX = shouldFlipX;
        }
        else
        {
            Debug.LogError("SpriteRenderer component not found on " + gameObject.name);
        }
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
