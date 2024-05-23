using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Combat : CoreComponent, IDamageable
{
    Collider2D col;
    IDamageable.DamagerTarget damagerTarget;
    IDamageable.KnockbackType knockbackType;

    Health health {get => _heath ??= core.GetCoreComponent<Health>(); }
    Health _heath;

    GameSettings settings;
    TouchCombat touchCombat;

    private bool canTouchCombat = true;

    Vector2 attackPosition;


    #region Set up
    
    public void SetUpCombatComponent(IDamageable.DamagerTarget damagerTarget, IDamageable.KnockbackType knockbackType)
    {
        this.damagerTarget = damagerTarget;
        this.knockbackType = knockbackType;
        
        if (damagerTarget == IDamageable.DamagerTarget.Player) return ;
        touchCombat = new TouchCombat(col, damagerTarget);
    }

    protected override void Awake()
    {
        base.Awake();

        col = GetComponent<Collider2D>();
    }

    void Start() 
    {
        settings = FindObjectOfType<GameSettings>();
    }



    #endregion

    #region Damage Method
    

    public IDamageable.DamagerTarget GetDamagerType()
    {
        return damagerTarget;
    }
    
    public void TakeDamage(int damage, IDamageable.DamagerTarget damagerType, Vector2 attackDirection, AspectType aspectType)
    {
        TakeDamage(damage, damagerType, attackDirection, false);
    }

    public void TakeDamage(int damage, IDamageable.DamagerTarget damagerType, Vector2 attackDirection, bool needResetPlayerPosition)
    {
        if (!CanAttack(damagerType)) return;
        if(!health.TakeDamage(damage, needResetPlayerPosition)) return;
    }

    bool CanAttack(IDamageable.DamagerTarget damagerType)
    {
        if (damagerTarget == damagerType) return false;
        if ((damagerTarget == IDamageable.DamagerTarget.Player &&
             damagerType == IDamageable.DamagerTarget.TamedMonster) ||
            damagerTarget == IDamageable.DamagerTarget.TamedMonster &&
            damagerType == IDamageable.DamagerTarget.Player) return false;

        return true;
    }

    #endregion
    

    public IDamageable.KnockbackType GetKnockbackType()
    {
        return knockbackType;
    }

    public Vector2 GetPosition()
    {
        return transform.position;
    }

    public Health GetHealth()
    {
        return health;
    }

    #region Collider

    public void EnableCollider()
    {
        col.enabled = true;
    }

    public void DisableCollider()
    {
        col.enabled = false;
    }

    public void EnableTouchCombat()
    {
        canTouchCombat = true;
    }

    public void DisableTouchCombat()
    {
        canTouchCombat = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (canTouchCombat)
        {
            touchCombat?.TouchAttack(other);
        }
    }

    #endregion
}
