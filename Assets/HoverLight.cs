using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverLight : MonoBehaviour
{
    public Transform rightPost;
    public Transform leftPost;
    public Vector3 startOffset;
    public Vector3 endOffset;
    public float yOffset = 0f; // Y position offset for the platform

    private bool movingToRight = true;
    public float moveSpeed = 2f; // Adjust this value to control the platform's movement speed
    public float pauseDuration = 1f; // Duration of the pause in seconds
    private float pauseTimer = 0f;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        // Calculate the start position based on the left post, startOffset, and yOffset
        Vector3 startPosition = leftPost.position + startOffset + new Vector3(0f, yOffset, 0f);
        transform.position = startPosition;

        // Get the SpriteRenderer component attached to the platform
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (pauseTimer > 0f)
        {
            // Platform is paused, decrement the pause timer
            pauseTimer -= Time.deltaTime;
            return;
        }

        // Calculate the target position based on the movement direction, offsets, and yOffset
        Vector3 targetPosition = movingToRight
            ? rightPost.position + endOffset + new Vector3(0f, yOffset, 0f)
            : leftPost.position + startOffset + new Vector3(0f, yOffset, 0f);

        // Move the platform towards the target position
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // Flip the sprite on the X-axis when moving right
        spriteRenderer.flipX = movingToRight;

        // Check if the platform has reached the target position
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            movingToRight = !movingToRight;
            pauseTimer = pauseDuration; // Start the pause timer
        }
    }
}