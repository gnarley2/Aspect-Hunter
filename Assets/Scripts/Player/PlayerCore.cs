using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCore : MonoBehaviour
{
    [SerializeField] private HealthData healthData;
    private Core core;

    private Health health;
    private Combat combat;

    private void Awake()
    {
        core = GetComponent<Core>();
    }

    private void Start()
    {
        combat = core.GetCoreComponent<Combat>();
        health = core.GetCoreComponent<Health>();
        SetUp();
    }

    void SetUp()
    {
        SetupHealthComponent();
        SetupCombatComponent();
    }

    void SetupHealthComponent()
    {
        health.SetHealth(healthData);
    }

    void SetupCombatComponent()
    {
        combat.SetUpCombatComponent(IDamageable.DamagerTarget.Player, IDamageable.KnockbackType.player); 
    }
}
