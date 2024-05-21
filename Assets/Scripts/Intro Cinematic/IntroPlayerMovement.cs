using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroPlayerMovement : MonoBehaviour
{
    public float speed = 5f;  // Speed at which the player will move to the right
    private bool shouldMove = true;  // Flag to determine if the object should keep moving
    private Animator animator;  // Reference to the Animator component
    public float destroyDelay = 2f;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldMove)
        {
            // Move the player to the right at a constant speed
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
    }

    // This method is called when the Collider2D enters the trigger
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "StopBox")
        {
            // Stop the movement
            shouldMove = false;
            animator.SetTrigger("Stop");
            StartCoroutine(DestroyAfterTime(destroyDelay));
        }
    }
    // Coroutine to destroy the GameObject after a delay
    IEnumerator DestroyAfterTime(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}