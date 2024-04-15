using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using static MoveNode;
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
    GameObject projectile;

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
        if (projectilePrefab.name == "Fire_Projectile")
        {
            rangeDamage = 15;
            projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            projectile.transform.up = attackDirection;
            Fire_Projectile fireprojectileMovement = projectile.GetComponent<Fire_Projectile>();
            fireprojectileMovement.Initialize(attackDirection, rangeDamage);
        }
        if (projectilePrefab.name == "Poison_Projectile")
        {
            rangeDamage = 0;
            projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            projectile.transform.up = attackDirection;
            Poison_Projectile poisonprojectileMovement = projectile.GetComponent<Poison_Projectile>();
            poisonprojectileMovement.Initialize(attackDirection, rangeDamage);
        }
        if (projectilePrefab.name == "Frost_Wall")
        {
            rangeDamage = 0;
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            projectile = Instantiate(projectilePrefab, mousePosition, Quaternion.identity);
            Frost_Wall frostwallSpawn = projectile.GetComponent<Frost_Wall>();
            frostwallSpawn.Initialize(Vector3.zero, rangeDamage);
        }
        if (projectilePrefab.name == "Shock_Hit")
        {
            rangeDamage = 15;
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            mousePosition.y += 1;
            projectile = Instantiate(projectilePrefab, mousePosition, Quaternion.identity);
            Shock_Hit shockhit = projectile.GetComponent<Shock_Hit>();
            shockhit.Initialize(Vector3.zero, rangeDamage);
        }
        if (projectilePrefab.name == "Water_Splash")
        {
            rangeDamage = 10;
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
         //   mousePosition.y += 1;
            projectile = Instantiate(projectilePrefab, mousePosition, Quaternion.identity);
            Water_Splash waterSplash = projectile.GetComponent<Water_Splash>();
            waterSplash.Initialize(Vector3.zero, rangeDamage);
        }
        if (projectilePrefab.name == "projectile")
        {
            rangeDamage = 10;
            projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            projectile.transform.up = attackDirection;
            ProjectileMovement projectileMovement = projectile.GetComponent<ProjectileMovement>(); // Pass the direction to the ProjectileMovement script
            projectileMovement.Initialize(attackDirection, rangeDamage);
        }
    }

    void MeleeAttack(Vector3 attackDirection)
    {
        GameObject meleeWeapon = Instantiate(meleePrefab, transform.position, Quaternion.identity);
        // Calculate the direction based on the mouse position
        meleeWeapon.transform.up = attackDirection;
       // Vector3 directionToMouse = (attackDirection - transform.position).normalized;
       
        MeleeMovement meleeMovement = meleeWeapon.GetComponent<MeleeMovement>();
        meleeMovement.Initialize(transform, attackDirection, meleeDamage);
    }
}

