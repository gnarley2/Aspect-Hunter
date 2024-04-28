using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frost_Wall : MonoBehaviour

{
    [SerializeField] private float wallDuration = 5f; // Duration of the wall in seconds
private Animator animator;
private int damage = 1;

// Start is called before the first frame update
void Start()
{
    animator = GetComponent<Animator>();
    StartCoroutine(DestroyWallAfterDuration());
}

// Coroutine to destroy the wall after a specified duration
IEnumerator DestroyWallAfterDuration()
{
    // Wait for the specified duration
    yield return new WaitForSeconds(wallDuration);
    // Set the "Destroy" boolean parameter to true in the animator
    animator.SetBool("Destroy", true);
    // Wait for a short delay to ensure the animation has played
    yield return new WaitForSeconds(1.5f);
    // Destroy the game object
    Destroy(gameObject);
}

// Function to initialize the Frost Wall with direction and damage
public void Initialize(Vector3 newDirection, int damage)
{
    this.damage = damage;
    // Lock the rotation of the Frost Wall
   transform.rotation = Quaternion.identity;
}
}