using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerHealth : MonoBehaviour
{
    public Health health;
    
    private void Start()
    {
        health = GetComponentInChildren<Health>();
        health.OnUpdateHealth += OnUpdateHealth;
        health.OnDie += OnDie;
    }

    public void TakeDamage(int value)
    {
        health.TakeDamage(value);
    }

    void OnUpdateHealth(int value)
    {
        
    }

    void OnDie()
    {
        CheckpointManager.Instance.LoadLastCheckpoint();
    }
    

}
