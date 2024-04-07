using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    public enum BallType
    {
        Common,
        Uncommon,
        Good,
        Epic,
        Legendary
    }

    [SerializeField] private BallType ballType;
    [SerializeField] private float speed;
    [SerializeField] private float maxDistance = 10f;

    private Vector2 initialPosition;
    private Vector2 direction;

    private void Awake()
    {
        initialPosition = transform.position;
    }

    public void Initialize(Vector3 newDirection, BallType ballType)
    {
        SetDirection(newDirection);
        this.ballType = ballType;
    }
    
    // Set the direction of movement for the projectile
    public void SetDirection(Vector3 newDirection)
    {
        direction = newDirection.normalized;
        // Optionally, rotate the projectile to face the direction
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void Update()
    {
        Move();
    }

    void Move()
    {
        // Move the projectile in the specified direction
        transform.Translate(direction * (speed * Time.deltaTime), Space.World);

        if (Vector3.Distance(initialPosition, transform.position) >= maxDistance)
        {
            Destroy(gameObject); // Destroy the projectile
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Enemy>(out Enemy enemy))
        {
            InventoryManager.Instance.AddMonster(enemy.GetMonsterData());
            Destroy(gameObject);
        }
    }
}
