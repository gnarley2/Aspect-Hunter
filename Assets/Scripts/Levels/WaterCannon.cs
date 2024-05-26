using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCannon : MonoBehaviour
{
    [SerializeField] private Vector2 box;
    [SerializeField] private Vector2 offset;
    [SerializeField] private int damage;

    private SpriteRenderer spriteRenderer;
    private GameObject player;
    private bool isFlipped;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");

        // Check the player's position and flip the SpriteRenderer if necessary
        FlipSpriteRenderer();
    }

    void DealDamage()
    {
        // Calculate the correct offset based on the flipped state
        Vector2 damageOffset = isFlipped ? new Vector2(-offset.x, offset.y) : offset;

        Collider2D[] cols = Physics2D.OverlapBoxAll(transform.position + (Vector3)damageOffset, box, 0);
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
        // Calculate the correct offset for the gizmos based on the flipped state
        Vector2 gizmosOffset = isFlipped ? new Vector2(-offset.x, offset.y) : offset;
        Gizmos.DrawWireCube(transform.position + (Vector3)gizmosOffset, box);
    }

    void Destroy()
    {
        Destroy(gameObject);
    }

    private void FlipSpriteRenderer()
    {
        if (player != null)
        {
            // Check if the player is on the left or right side
            if (transform.position.x < player.transform.position.x)
            {
                // Player is on the right side, flip the SpriteRenderer horizontally
                spriteRenderer.flipX = true;
                isFlipped = true;
            }
            else
            {
                // Player is on the left side, don't flip the SpriteRenderer
                spriteRenderer.flipX = false;
                isFlipped = false;
            }
        }
    }
}