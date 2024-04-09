using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    [SerializeField] private float arcRotationSpeed = 5f;
    [SerializeField] private float flashlightRotationSpeed = 10f;
    [SerializeField] private float arcRadius = 2f;
    [SerializeField] private float arcAngle = 180f;

    private Transform playerTransform;
    private float currentAngle;
    private Quaternion targetRotation;

    void Start()
    {
        playerTransform = Camera.main.transform;
    }

    void Update()
    {
        // Calculate the target position on the arc
        Vector3 playerPos = playerTransform.position;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 direction = (mousePos - playerPos).normalized;
        currentAngle = Vector2.SignedAngle(Vector2.right, direction);
        currentAngle = Mathf.Clamp(currentAngle, -arcAngle / 2, arcAngle / 2);

        float x = playerPos.x + arcRadius * Mathf.Cos(currentAngle * Mathf.Deg2Rad);
        float y = playerPos.y + arcRadius * Mathf.Sin(currentAngle * Mathf.Deg2Rad);

        // Move the flashlight to the target position
        transform.position = Vector3.Lerp(transform.position, new Vector3(x, y, transform.position.z), arcRotationSpeed * Time.deltaTime);

        // Rotate the flashlight to point away from the player
        Vector3 targetDirection = transform.position - playerPos;
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // Smoothly rotate the flashlight towards the target rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, flashlightRotationSpeed * Time.deltaTime);
    }
}