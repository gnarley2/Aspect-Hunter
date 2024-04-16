using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin_Orb : MonoBehaviour
{
    
    public float moveSpeed = 5f; // Speed at which the coin orb moves towards the player
    public float destroyDistance = 0.1f; // Distance threshold at which the coin orb destroys itself

    private Transform playerTransform; // Reference to the player's transform
    private bool isMovingTowardsPlayer; // Flag to check if the coroutine is running

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
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

        GameManager.Instance.AddCoin(); // Call the AddCoin function in the GameManager
        Destroy(gameObject);

        isMovingTowardsPlayer = false;
    }
}
