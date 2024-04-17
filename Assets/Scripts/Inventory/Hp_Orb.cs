using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class Hp_Orb : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed at which the XP orb moves towards the player
    public float destroyDistance = 0.1f; // Distance threshold at which the XP orb destroys itself

    private Transform playerTransform; // Reference to the player's transform
    private bool isMovingTowardsPlayer; // Flag to check if the coroutine is running

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

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

        // AddItem Health to Player here!!

        Destroy(gameObject);
        isMovingTowardsPlayer = false;
    }

}
