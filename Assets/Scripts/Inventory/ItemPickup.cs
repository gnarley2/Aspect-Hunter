using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item Item;
    private Collider2D itemCollider;
    public float moveSpeed = 5f; // Speed at which the item moves towards the player
    public float destroyDistance = 0.1f; // Distance threshold at which the item destroys itself

    private Transform playerTransform; // Reference to the player's transform
    private bool isMovingTowardsPlayer; // Flag to check if the coroutine is running

    private void Start()
    {
        itemCollider = GetComponent<Collider2D>();
    }

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

        InventoryManager.Instance.Add(Item); // Add the item to the inventory
        Destroy(gameObject);

        isMovingTowardsPlayer = false;
    }
}