using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyData data;
    
    private Collider2D col;
    private Core core;
    private Health health;
    private Combat combat;

    void Awake()
    {
        core = GetComponentInChildren<Core>();
        data = data.Clone();
    }

    void Start() 
    {
        combat = core.GetCoreComponent<Combat>();
        health = core.GetCoreComponent<Health>();
        SetupComponent();

        AddEvent();
    }
    
    public void SetupComponent()
    {
        combat.SetUpCombatComponent(IDamageable.DamagerTarget.Enemy, data.KnockbackType);

        if (data == null) return;
        health.SetHealth(data.healthData);
        data.currentHealth = data.healthData.maxHealth;
    }

    public void AddEvent()
    {
        health.OnTakeDamage += OnTakeDamage;
        health.OnUpdateHealth += data.UpdateCurrentHealth;
        health.OnDie += Die;
    }

    #region Tamed
        public void Initialize(EnemyData data)
        {
            this.data = data;
            GetComponent<BehaviourTreeRunner>().tree = this.data.monsterData.tamedTree;
        }

    #endregion

    private void Die()
    {
        PlayHitClip();
        
        // todo spawn loot
        Destroy();
    }

    public void Destroy()
    {
        transform.parent.gameObject.SetActive(false);
    }

    #region MonsterData

    public EnemyData GetData()
    {
        return data;
    }

    #endregion

    #region Events

    private void OnTakeDamage(bool obj = false)
    {
        PlayHitClip();
    }

    #endregion

    #region Play Sound
    
    private void PlayHitClip()
    {
        // todo playsound
    }

    #endregion
}
