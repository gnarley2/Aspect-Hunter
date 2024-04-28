using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeMovement : MonoBehaviour
{
    private Vector3 direction;
    private Transform playerTransform;

    [SerializeField] public float radius = 2f; // Radius of the arc
    [SerializeField] public float speed = 20f; // Speed of movement along the arc
    [SerializeField] private float maxAngle = 20f;

    private float angle = 0f; // Current angle on the circle
    private float initialAngle = 0f; // Initial angle for the arc
    private int damage;

    public void Initialize(Transform playerTransform, Vector3 initialDirection, int damage)
    {
        this.playerTransform = playerTransform;
        SetInitialDirection(initialDirection);
        this.damage = damage;
    }

    // Update is called once per frame
    void Update()
    {
        // Ensure that playerTransform is not null before proceeding
        if (playerTransform == null)
        {
            return;
        }

        MeleeArc();
    }

    void MeleeArc()
    {
        angle += speed * Time.deltaTime;
        float currentAngle = initialAngle + angle;
        float x = playerTransform.position.x + radius * Mathf.Cos(currentAngle * Mathf.Deg2Rad);
        float y = playerTransform.position.y + radius * Mathf.Sin(currentAngle * Mathf.Deg2Rad);
        transform.position = new Vector3(x, y, 0f);

        // Rotate the sprite based on the angle from the center
        float spriteAngle = Mathf.Atan2(y - playerTransform.position.y, x - playerTransform.position.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(spriteAngle, Vector3.forward);

        if (angle >= maxAngle)
        {
            Destroy(gameObject); // Destroy the melee object
        }
    }

    public void SetInitialDirection(Vector3 initialDirection)
    {
        initialAngle = Mathf.Atan2(initialDirection.y, initialDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, initialDirection);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<IDamageable>(out IDamageable target))
        {
            target.TakeDamage(damage, IDamageable.DamagerTarget.Player, Vector2.zero);
            PlayerCombat.SetTarget(target);
        }
    }
}