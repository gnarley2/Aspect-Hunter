using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;


public class PlayerCombat : MonoBehaviour
{
    private Vector3 mousePosition;
    private Vector3 direction;
    
    [Header("Range")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private int rangeDamage = 10;
    
    [Header("Melee")]
    [SerializeField] private GameObject meleePrefab;
    [SerializeField] private int meleeDamage = 15;

    [Header("Ball")] [SerializeField] private GameObject ballPrefab;
    
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

            if (Input.GetMouseButtonDown(1))
            {
                MeleeAttack(direction);
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                CatchMonster(direction);
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                ReleaseMonster(direction);
            }
        }
    }

    void CalculateDirection()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = (mousePosition - transform.position).normalized;
    }

    void ProjectileAttack(Vector2 attackDirection)
    {
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        projectile.transform.up = attackDirection;
         
        ProjectileMovement projectileMovement = projectile.GetComponent<ProjectileMovement>();       
        
        // Pass the direction to the ProjectileMovement script
        projectileMovement.Initialize(attackDirection, IDamageable.DamagerTarget.Player, rangeDamage);
    }

    void MeleeAttack(Vector2 attackDirection)
    {
        GameObject meleeWeapon = Instantiate(meleePrefab, transform.position, Quaternion.identity);
        // Calculate the direction based on the mouse position
        meleeWeapon.transform.up = attackDirection;
       // Vector3 directionToMouse = (attackDirection - transform.position).normalized;
       
        MeleeMovement meleeMovement = meleeWeapon.GetComponent<MeleeMovement>();
        meleeMovement.Initialize(transform, attackDirection, meleeDamage);
    }

    void CatchMonster(Vector2 throwPosition)
    {
        GameObject projectile = Instantiate(ballPrefab, transform.position, Quaternion.identity);
        projectile.transform.up = throwPosition;
         
        BallMovement projectileMovement = projectile.GetComponent<BallMovement>();       
        
        // Pass the direction to the ProjectileMovement script
        projectileMovement.Initialize(throwPosition, BallMovement.BallType.Uncommon);
    }

    void ReleaseMonster(Vector2 throwPosition)
    {
        GameObject projectile = Instantiate(ballPrefab, transform.position, Quaternion.identity);
        projectile.transform.up = throwPosition;
         
        BallMovement projectileMovement = projectile.GetComponent<BallMovement>();
        
        if (InventoryManager.Instance.HasMonster(0))
            // Pass the direction to the ProjectileMovement script
            projectileMovement.Initialize(throwPosition, InventoryManager.Instance.GetMonster(0));
    }
}

