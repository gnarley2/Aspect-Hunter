using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerHealth : MonoBehaviour
{
    public float health = 0f;
    public float maxHealth = 100f;
    public event System.Action<float> OnHealthChanged;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateHealth(float mod)
    {
        health += mod;

        if (health > maxHealth)
        {
            health = maxHealth;
        }
        else if (health <= 0f)
        {
            health = 0f;
            Debug.Log("Player Died");
        }

        // Invoke the OnHealthChanged event with the new health value
        OnHealthChanged?.Invoke(health);
    }

    public float GetHealth()
    {
        return health;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }
}
