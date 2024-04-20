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

    private int holdingMonsterIndex = -1; // for releasing monster

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
        this.holdingMonsterIndex = monsterIndex;
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

            if (holdingMonsterIndex != -1)
            {
                ReleaseMonster();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (holdingMonsterIndex != -1) return;
        
        if (other.TryGetComponent<Monster>(out Monster enemy))
        {
            ProcessMonster(enemy);
        }
    }

    void ProcessMonster(Monster monster)
    {
        
        if (monster.monsterIndex == -1)
        {
            if (CanCatchMonster(monster))
            {
                CatchMonster(monster);
                monster.Destroy();
            }
        }
        else
        {
            UnReleaseMonster(monster);
            monster.Destroy();
        }
        
        Destroy(gameObject);
    }

    void CatchMonster(Monster monster)
    {
        MonsterInventory.Instance.AddMonster(monster.GetData());
    }

    bool CanCatchMonster(Monster monster)
    {
        if (monster.GetHealthPercent() > 0.5)
        {
            InformationPanel.Instance.ShowInformation("Monster's health is too high");
            return false;
        }
        return true;
    }
    
    void UnReleaseMonster(Monster monster)
    {
        MonsterInventory.Instance.UnReleaseMonster(monster, monster.monsterIndex);
    }

    void ReleaseMonster()
    {
        MonsterInventory.Instance.ReleaseMonster(holdingMonsterIndex);
    }
}
