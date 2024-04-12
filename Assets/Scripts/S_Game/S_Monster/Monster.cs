using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Monster : MonoBehaviour
{
    [SerializeField] private MonsterData data;
    public IDamageable.DamagerTarget damagerTarget = IDamageable.DamagerTarget.Enemy;
    
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

        if (data == null) return;
        SetupComponent();
        AddEvent();
    }
    
    public void SetupComponent()
    {
        combat.SetUpCombatComponent(damagerTarget, data.KnockbackType);

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
        public void InitializeUponReleasing(MonsterData data)
        {
            damagerTarget = IDamageable.DamagerTarget.Player;
            StartCoroutine(InitializeCoroutine(data));
        }

        IEnumerator InitializeCoroutine(MonsterData data)
        {
            this.data = data;
            yield return new WaitUntil(() => combat != null);
            
            SetupComponent();
            AddEvent();
            GetComponent<BehaviourTreeRunner>().InitializeTree(data.monsterDetails.tamedTree);
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

    #region MonsterDetails

    public MonsterData GetData()
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
