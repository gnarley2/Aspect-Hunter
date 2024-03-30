using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEditor;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;


public class PlayerCombat : MonoBehaviour
{
    private Vector3 mousePosition;
    private Vector3 direction;
    [SerializeField] private GameObject prefab;

    void Update()
    {
        CalculateDirection();
        if (Input.GetKeyDown(KeyCode.Space)||Input.GetMouseButtonDown(0))
        {
          
            Attack(direction);
        }
    }

    void CalculateDirection()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = (mousePosition - transform.position).normalized;
    }

    void Attack(Vector3 attackDirection)
    {
        GameObject projectile = Instantiate(prefab, transform.position, Quaternion.identity);
        projectile.transform.up = attackDirection;
        ProjectileMovement projectileMovement = projectile.GetComponent<ProjectileMovement>();       // Pass the direction to the ProjectileMovement script
      
        if (projectileMovement != null)
        {
            projectileMovement.SetDirection(attackDirection);
        }
    }
}

