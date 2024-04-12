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

    private int monsterIndex = -1; // for releasing monster

    private void Awake()
    {
        initialPosition = transform.position;
    }
    
    public void Initialize(Vector3 newDirection, BallType ballType)
    {
        SetDirection(newDirection);
        this.ballType = ballType;
    }

    public void Initialize(Vector3 newDirection, int monsterIndex)
    {
        SetDirection(newDirection);
        this.monsterIndex = monsterIndex;
        maxDistance = 2f;
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

            if (monsterIndex != -1)
            {
                ReleaseMonster();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (monsterIndex != -1) return;
        
        if (other.TryGetComponent<Monster>(out Monster enemy))
        {
            CatchMonster(enemy);   
        }
    }

    void CatchMonster(Monster monster)
    {
        InventoryManager.Instance.AddMonster(monster.GetData());
        monster.Destroy();
        Destroy(gameObject);
    }

    void ReleaseMonster()
    {
        InventoryManager.Instance.ReleaseMonster(monsterIndex);
    }
}
