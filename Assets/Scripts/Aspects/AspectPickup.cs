using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AspectPickup : MonoBehaviour
{
    
    public Aspect Aspect;
    private Collider2D aspectCollider;
    public float moveSpeed = 5f; // Speed at which the aspect moves towards the player
    public float destroyDistance = 0.1f; // Distance threshold at which the aspect destroys itself

    private Transform playerTransform; // Reference to the player's transform
    private bool isMovingTowardsPlayer; // Flag to check if the coroutine is running

    private void Start()
    {
        aspectCollider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && aspectCollider != null)
        {
            Debug.Log("Aspect Collision Detected with Player");
            playerTransform = other.transform; // Get the player's transform
            if (!isMovingTowardsPlayer)
            {
                StartCoroutine(VacuumEffect());
            }
        }
    }

    private IEnumerator VacuumEffect()
    {
        isMovingTowardsPlayer = true;

        while (Vector3.Distance(transform.position, playerTransform.position) > destroyDistance)
        {
            Vector3 direction = (playerTransform.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;
            yield return null;
        }

        Pickup();

        isMovingTowardsPlayer = false;
    }

    private void Pickup()
    {
        AspectManager.Instance.Add(Aspect);
        Destroy(gameObject);
    }
}
