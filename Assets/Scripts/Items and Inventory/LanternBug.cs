using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanternBug : MonoBehaviour
{
    [SerializeField] private float orbitRadius = 2f; // Radius of the orbit circle
    [SerializeField] private float orbitSpeed = 90f; // Speed of rotation in degrees per second

    private Transform playerTransform;
    private float currentAngle;

    void Start()
    {
        playerTransform = Camera.main.transform; // Assuming the player is at the camera position
    }

    void Update()
    {
        // Calculate the target position on the orbit circle
        Vector3 playerPos = playerTransform.position;
        currentAngle += orbitSpeed * Time.deltaTime;
        currentAngle = currentAngle % 360f; // Keep the angle within 0-360 degrees

        float x = playerPos.x + orbitRadius * Mathf.Cos(currentAngle * Mathf.Deg2Rad);
        float y = playerPos.y + orbitRadius * Mathf.Sin(currentAngle * Mathf.Deg2Rad);

        // Move the lanternbug to the target position
        transform.position = new Vector3(x, y, transform.position.z);

        // Prevent the lanternbug from rotating
        transform.rotation = Quaternion.identity;
    }
}