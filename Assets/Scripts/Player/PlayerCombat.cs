using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;


public class PlayerCombat : MonoBehaviour
{
    private Vector3 mousePosition;
    private Vector3 direction;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private int rangeDamage = 10;
    [SerializeField] private GameObject meleePrefab;
    [SerializeField] private int meleeDamage = 15;
    
    
    public Menus menuScript; // Reference to the Menu script
    
    void Update()
    {
        GameObject menuObject = GameObject.Find("UIManager");
        menuScript = menuObject.GetComponent<Menus>();
        if (menuScript.isPaused == false)
        {
            CalculateDirection();
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {

                ProjectileAttack(direction);
            }

            if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(1))
            {
                MeleeAttack(direction);
            }
        }
    }

    void CalculateDirection()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = (mousePosition - transform.position).normalized;
    }

    void ProjectileAttack(Vector3 attackDirection)
    {
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        projectile.transform.up = attackDirection;
        ProjectileMovement projectileMovement = projectile.GetComponent<ProjectileMovement>();       // Pass the direction to the ProjectileMovement script
      
        if (projectileMovement != null)
        {
            projectileMovement.SetDirection(attackDirection);
        }
    }

    void MeleeAttack(Vector3 attackDirection)
    {
        GameObject meleeWeapon = Instantiate(meleePrefab, transform.position, Quaternion.identity);
        // Calculate the direction based on the mouse position
        meleeWeapon.transform.up = attackDirection;
       // Vector3 directionToMouse = (attackDirection - transform.position).normalized;
        MeleeMovement meleeMovement = meleeWeapon.GetComponent<MeleeMovement>();
        if (meleeMovement != null)
        {
            // Pass the direction to the MeleeMovement script
            meleeMovement.Initialize(transform, attackDirection, meleeDamage);
        }
    }
}

