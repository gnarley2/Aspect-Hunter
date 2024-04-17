using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire_Projectile_Hit : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        if (animator != null)
        {
            StartCoroutine(DestroyAfterAnimation());
        }
        else
        {
            Debug.LogWarning("No Animator component found on the game object. The game object will not be destroyed automatically.");
        }
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
}
