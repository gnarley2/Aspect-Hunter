using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Monster : MonoBehaviour
{
    [SerializeField] private MonsterData data;
    public IDamageable.DamagerTarget damagerTarget = IDamageable.DamagerTarget.Enemy;
    public int monsterIndex = -1;
    
    private Collider2D col;
    private Core core;
    private Health health;
    private Combat combat;

    public Action OnDied;

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
        public void InitializeUponReleasing(MonsterData data, int index)
        {
            damagerTarget = IDamageable.DamagerTarget.TamedMonster;
            monsterIndex = index;
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

        public void UnRelease()
        {
            //InventoryManager.Instance.UnReleaseMonster(this, monsterIndex);
        }

    #endregion

    private void Die()
    {
        OnDied?.Invoke();
        PlayHitClip();
        
        Destroy();
    }

    public void Destroy()
    {
        // todo spawn loot
        Destroy(transform.parent.gameObject);
        
        // todo adding pool object   
        // transform.parent.gameObject.SetActive(false);
    }

    #region MonsterDetails

    public MonsterData GetData()
    {
        return data;
    }

    public float GetHealthPercent()
    {
        return health.GetPercent();
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
