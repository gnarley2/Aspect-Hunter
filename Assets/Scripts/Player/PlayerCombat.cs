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
    [SerializeField] private int rangeDamage = 10;
    [SerializeField] private float speed = 15f;
    
    [Header("Melee")]
    [SerializeField] private GameObject meleePrefab;
    [SerializeField] private int meleeDamage = 15;

    [Header("Ball")] [SerializeField] private GameObject ballPrefab;
    
    public Menus menuScript;

    [SerializeField] public int currentProjectileIndex = 0;
    [SerializeField] private ProjectileData[] projectileDatas;

    private void Start()
    {
        foreach (ProjectileData data in projectileDatas)
        {
            MonsterInventory.Instance.OnCatchMonster += data.Unlock;
        }
    }

    void Update()
    {
        if (menuScript && menuScript.isPaused == true) return;
        
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

        // if (Input.GetKeyDown(KeyCode.E))
        // {
        //     ReleaseMonster(direction);
        // }
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
        ProjectileData selectedProjectile = projectileDatas[currentProjectileIndex];
        if (!selectedProjectile.isUnlocked)
        {
            InformationPanel.Instance.ShowInformation($"{selectedProjectile.name} is locked");
            return;
        }
        
        AspectType currentType = selectedProjectile.type;
        GameObject projectilePrefab = selectedProjectile.prefab;

        
        // Perform additional actions based on the projectile type
        switch (currentType)
        {
            case AspectType.Fire:

                GameObject fireprojectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                Fire_Projectile fireMovement = fireprojectile.GetComponent<Fire_Projectile>();
                fireMovement.Initialize(attackDirection, rangeDamage);
                fireprojectile.transform.up = attackDirection;
                break;

            case AspectType.Frost:

                GameObject frostprojectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                Frost_Wall frostWall = frostprojectile.GetComponent<Frost_Wall>();
                frostWall.Initialize(attackDirection, rangeDamage);
                frostprojectile.transform.up = attackDirection;
                break;


            case AspectType.Poison:

                GameObject poisonprojectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                Poison_Projectile poisonMovement = poisonprojectile.GetComponent<Poison_Projectile>();
                poisonMovement.Initialize(attackDirection, rangeDamage);
                poisonprojectile.transform.up = attackDirection;


                break;
            case AspectType.Shock:

                GameObject shockprojectile = Instantiate(projectilePrefab, GetMouseWorldPosition(), Quaternion.identity);
                Shock_Hit shockHit = shockprojectile.GetComponent<Shock_Hit>();
                shockHit.Initialize(attackDirection, rangeDamage);


                break;
            case AspectType.Water:
           
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                worldPosition.z = 0f; 
                GameObject waterprojectile = Instantiate(projectilePrefab, worldPosition, Quaternion.identity);
                Water_Splash waterSplash = waterprojectile.GetComponent<Water_Splash>();
                Vector3 attackDirectionWater = (worldPosition - transform.position).normalized;
                waterSplash.Initialize(attackDirectionWater, rangeDamage, transform);
              //  waterprojectile.transform.up = attackDirectionWater;

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

