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
    public static IDamageable Target {
        get; private set; }

    public static void SetTarget(IDamageable target)
    {
        Target = target;
        target.GetHealth().OnDie += RemoveTarget;
    }

    static void RemoveTarget()
    {
        Target = null;
    }

    private Vector3 mousePosition;
    private Vector3 direction;
    private Vector3 attackDirection;

    [Header("Range")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private int rangeDamage = 10;
    [SerializeField] private float speed = 15f;
    
    [Header("Melee")]
    [SerializeField] private GameObject meleePrefab;
    [SerializeField] private int meleeDamage = 15;

    [Header("Ball")] [SerializeField] private GameObject ballPrefab;
    
    public Menus menuScript; // Reference to the Menu script

    public enum ProjectileType
    {
        Fire,
        Frost,
        Shock,
        Poison,
        Water,
        None,
    }
    
    [SerializeField] public ProjectileType currentProjectileType = ProjectileType.Fire;
    [SerializeField] private GameObject[] projectilePrefabs;

    void Update()
    {
        // GameObject menuObject = GameObject.Find("UIManager");
        // menuScript = menuObject.GetComponent<Menus>();
        // if (menuScript.isPaused == false)
        // {
        // }
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

    Vector2 GetMouseWorldPosition()
    {
        return Camera.main.ScreenPointToRay(Input.mousePosition).origin;
    }

    void CalculateDirection()
    {
        // Get the mouse position in world coordinates
        Vector3 mouseWorldPosition = Camera.main.ScreenPointToRay(Input.mousePosition).origin;
        // Calculate the direction from the player to the mouse position
        direction = (mouseWorldPosition - transform.position).normalized;
    }

    void ProjectileAttack(Vector3 attackDirection)
    {
        CalculateDirection();
        GameObject projectilePrefab = projectilePrefabs[(int)currentProjectileType];
        
        // Perform additional actions based on the projectile type
        switch (currentProjectileType)
        {
            case ProjectileType.Fire:

                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePos.z = 0; 
                Vector3 direction = (mousePos - transform.position).normalized;
                GameObject fireprojectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                Fire_Projectile fireMovement = fireprojectile.GetComponent<Fire_Projectile>();
                fireMovement.Initialize(direction, rangeDamage);

                break;

            case ProjectileType.Frost:

                GameObject frostprojectile = Instantiate(projectilePrefab, GetMouseWorldPosition(), Quaternion.identity);
                Frost_Wall frostWall = frostprojectile.GetComponent<Frost_Wall>();
                frostWall.Initialize(attackDirection, rangeDamage);
    
                break;

            case ProjectileType.Poison:

              //  Vector3 mousePos1 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
             //   mousePos.z = 0;
             //   Vector3 direction1 = (mousePos1 - transform.position).normalized;

                GameObject poisonprojectile = Instantiate(projectilePrefab, GetMouseWorldPosition(), Quaternion.identity);

                Poison_HIt poisonhit = poisonprojectile.GetComponent<Poison_HIt>();
              poisonhit.Initialize(attackDirection, rangeDamage);

                break;

            case ProjectileType.Shock:

                GameObject shockprojectile = Instantiate(projectilePrefab, GetMouseWorldPosition(), Quaternion.identity);
                Shock_Hit shockHit = shockprojectile.GetComponent<Shock_Hit>();
                shockHit.Initialize(attackDirection, rangeDamage);

                break;

            case ProjectileType.Water:

                GameObject waterprojectile = Instantiate(projectilePrefab, GetMouseWorldPosition(), Quaternion.identity);
                Water_Splash waterSplash = waterprojectile.GetComponent<Water_Splash>();
                waterSplash.Initialize(attackDirection, rangeDamage);
                
                break;
        }

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
        GameObject ball = Instantiate(ballPrefab, transform.position, Quaternion.identity);
        ball.transform.up = throwPosition;
         
        BallMovement ballMovement = ball.GetComponent<BallMovement>();
        
        //if (InventoryManager.Instance.HasMonster(0))
        //    // Pass the direction to the ProjectileMovement script
        //    ballMovement.Initialize(throwPosition, 0);
    }
}

