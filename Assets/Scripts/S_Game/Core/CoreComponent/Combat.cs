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

    Movement movement {get => _movement ??= core.GetCoreComponent<Movement>(); }
    Movement _movement;

    Health health {get => _heath ??= core.GetCoreComponent<Health>(); }
    Health _heath;

    GameSettings settings;
    TouchCombat touchCombat;

    private bool canTouchCombat = true;

    Vector2 attackPosition;
    Vector2 hitDirection;


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
    
    public void TakeDamage(int damage, IDamageable.DamagerTarget damagerType, Vector2 attackDirection)
    {
        TakeDamage(damage, damagerType, attackDirection, false);
    }

    public void TakeDamage(int damage, IDamageable.DamagerTarget damagerType, Vector2 attackDirection, bool needResetPlayerPosition)
    {
        if (this.damagerTarget == damagerType) return;
        if(!health.TakeDamage(damage, needResetPlayerPosition)) return;
        if (needResetPlayerPosition) return;

        if (attackDirection == Vector2.zero)
        {
            hitDirection = -movement.faceDirection;
        }
        else
        {
            hitDirection = attackDirection;
        }
        
    }

    #endregion
    

    public IDamageable.KnockbackType GetKnockbackType()
    {
        return knockbackType;
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
