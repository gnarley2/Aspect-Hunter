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

    [SerializeField] public int currentProjectileIndex = -1;
    [SerializeField] private ProjectileData[] projectileDatas;

    public HUDManager hudManager;

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

    public void SetCurrentProjectileIndexBasedOnAspect(EquippableItem aspectItem)
    {
        switch (aspectItem.aspectType)
        {
            case AspectType.Fire:
                currentProjectileIndex = 0;
                break;
            case AspectType.Frost:
                currentProjectileIndex = 1;
                break;
            case AspectType.Shock:
                currentProjectileIndex = 3;
                break;
            case AspectType.Poison:
                currentProjectileIndex = 2;
                break;
            case AspectType.Water:
                currentProjectileIndex = 4;
                break;
            case AspectType.Steam:
                currentProjectileIndex = 13;
                break;
            case AspectType.Blast:
                currentProjectileIndex = 5;
                break;
            case AspectType.Radiation:
                currentProjectileIndex = 12;
                break;
            case AspectType.Gas:
                currentProjectileIndex = 7;
                break;
            case AspectType.Paralysis:
                currentProjectileIndex = 10;
                break;
            case AspectType.Pollution:
                currentProjectileIndex = 11;
                break;
            case AspectType.Superconductor:
                currentProjectileIndex = 14;
                break;
            case AspectType.Necrotic:
                currentProjectileIndex = 9;
                break;
            case AspectType.Corrosion:
                currentProjectileIndex = 6;
                break;
            case AspectType.IceSpike:
                currentProjectileIndex = 8;
                break;
            // Continue for other aspects as necessary
            default:
                Debug.LogError("Aspect not recognized: " + aspectItem.ItemName);
                break;
        }
    }

    void ProjectileAttack(Vector3 attackDirection)
    {
        ProjectileData selectedProjectile = projectileDatas[currentProjectileIndex];
        if (currentProjectileIndex <= -1 || currentProjectileIndex >= projectileDatas.Length)
        {
            InformationPanel.Instance.ShowInformation($"You haven't selected an aspect");
            return;
        }

        // if (!selectedProjectile.isUnlocked)
        // {
        //     InformationPanel.Instance.ShowInformation($"{selectedProjectile.name} is locked");
        //     return;
        // }
        
        AspectType currentType = selectedProjectile.type;
        GameObject projectilePrefab = selectedProjectile.prefab;

        
        // Perform additional actions based on the projectile type
        if (!AspectInventory.Instance.UseAspect(currentType, 1)) return;
        switch (currentType)
        {
            case AspectType.Fire:
                GameObject fireprojectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                Fire_Projectile fireMovement = fireprojectile.GetComponent<Fire_Projectile>();
                fireMovement.Initialize(attackDirection, rangeDamage);
                fireprojectile.transform.up = attackDirection;

                break;

            case AspectType.Frost:
                Vector3 worldPositionF = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                worldPositionF.z = 0f;
                GameObject frostprojectile = Instantiate(projectilePrefab, worldPositionF, Quaternion.identity);
                Frost_Wall frostWall = frostprojectile.GetComponent<Frost_Wall>();
                frostWall.Initialize(attackDirection, rangeDamage);
          
                break;

            case AspectType.Poison:

                Vector3 worldPosition1 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                worldPosition1.z = 0f;
                GameObject poisonprojectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                Poison_Projectile poisonMovement = poisonprojectile.GetComponent<Poison_Projectile>();
               // Vector3 attackDirectionPoison = (worldPosition1 - transform.position).normalized;
                poisonMovement.Initialize(attackDirection, rangeDamage);



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

                break;

            case AspectType.Blast:

                GameObject blastprojectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                Blast_Projectile blastMovement = blastprojectile.GetComponent<Blast_Projectile>();
                blastMovement.Initialize(attackDirection, rangeDamage);
                blastMovement.transform.up = attackDirection;

                break;

            case AspectType.Corrosion:

                GameObject corrosionprojectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                Corrosive_Projectile corroMovement = corrosionprojectile.GetComponent<Corrosive_Projectile>();
                corroMovement.Initialize(attackDirection, rangeDamage);
                corroMovement.transform.up = attackDirection;

                break;

            case AspectType.Gas:

                Vector3 worldPositionGas = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                worldPositionGas.z = 0f;

                GameObject gasprojectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                Gas_Projectile gasMovement = gasprojectile.GetComponent<Gas_Projectile>();
               // Vector3 attackDirectionGas = (worldPositionGas - transform.position).normalized;
                gasMovement.Initialize(attackDirection, rangeDamage);

                break;


            case AspectType.IceSpike:

                GameObject spikeprojectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                Ice_Spike spikeMovement = spikeprojectile.GetComponent<Ice_Spike>();
                spikeMovement.Initialize(attackDirection, rangeDamage);
                spikeMovement.transform.up = attackDirection;


                break;

            case AspectType.Necrotic:

                GameObject necroprojectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                Necrotic_Projectile necroMovement = necroprojectile.GetComponent<Necrotic_Projectile>();
                necroMovement.Initialize(attackDirection, rangeDamage);
                necroMovement.transform.up = attackDirection;


                break;

            case AspectType.Paralysis:

                Vector3 worldPositionpara = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                worldPositionpara.z = 0f;
                GameObject paraprojectile = Instantiate(projectilePrefab, worldPositionpara, Quaternion.identity);
                Paralysis_Splash paraMovement = paraprojectile.GetComponent<Paralysis_Splash>();
              
            
               Vector3 attackDirectionpara = (worldPositionpara - transform.position).normalized;
                paraMovement.Initialize(attackDirectionpara, rangeDamage, transform);
               // paraMovement.transform.up = attackDirection;


                break;

            case AspectType.Pollution:

                GameObject pollprojectile = Instantiate(projectilePrefab, GetMouseWorldPosition(), Quaternion.identity);
                Pollution_Hit pollHit = pollprojectile.GetComponent<Pollution_Hit>();
                pollHit.Initialize(attackDirection, rangeDamage);


                break;

            case AspectType.Radiation:


                GameObject radprojectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                Radiation_Projectile radMovement = radprojectile.GetComponent<Radiation_Projectile>();
                radMovement.Initialize(attackDirection, rangeDamage);
                radMovement.transform.up = attackDirection;

                break;


            case AspectType.Steam:


                GameObject steamprojectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                Steam_Projectile steamMovement = steamprojectile.GetComponent<Steam_Projectile>();
                steamMovement.Initialize(attackDirection, rangeDamage);
                steamMovement.transform.up = attackDirection;

                break;


            case AspectType.Superconductor:


                GameObject superprojectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                SuperConductor_Projectile superMovement = superprojectile.GetComponent<SuperConductor_Projectile>();
                superMovement.Initialize(attackDirection, rangeDamage);
                superMovement.transform.up = attackDirection;

                break;
        }
        if (hudManager != null)
        {
            hudManager.UpdateAspectCount();
        }
    }

    void MeleeAttack(Vector2 attackDirection)
    {
        GameObject meleeWeapon = Instantiate(meleePrefab, transform.position, Quaternion.identity);
        meleeWeapon.transform.up = attackDirection;
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

