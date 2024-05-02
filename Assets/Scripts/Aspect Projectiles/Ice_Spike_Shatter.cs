using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice_Spike_Shatter : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Transform playerTransform; // Reference to the player's transform

    private void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Get the reference to the player's transform
        playerTransform = GameObject.FindWithTag("Player").transform;

        if (animator != null)
        {
            StartCoroutine(DestroyAfterAnimation());
        }
        else
        {
            Debug.LogWarning("No Animator component found on the game object. The game object will not be destroyed automatically.");
        }

        // Flip the sprite based on the position relative to the player
        FlipSpriteBasedOnPlayerPosition();
    }

    private IEnumerator DestroyAfterAnimation()
    {
        while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            yield return null;
        }

        // Animation has completed
        Destroy(gameObject);
    }

    private void FlipSpriteBasedOnPlayerPosition()
    {
        // Calculate the direction from the player to the game object
        Vector3 directionToPlayer = transform.position - playerTransform.position;

        // Flip the sprite if the game object is to the left of the player
        spriteRenderer.flipX = directionToPlayer.x < 0;
    }
}
