using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public PlayerHealth playerHealthScript;
    [SerializeField] private Image healthBar;

    private void Start()
    {
        if (playerHealthScript == null)
        {
            Debug.LogError("PlayerHealth script is not assigned!");
        }
        else
        {
            // Subscribe to the OnHealthChanged event
            playerHealthScript.OnHealthChanged += HandleHealthChanged;
        }
    }

    private void HandleHealthChanged(float newHealth)
    {
        // Update the health bar fill amount based on the new health value
        healthBar.fillAmount = newHealth / playerHealthScript.maxHealth;
    }

    private void Update()
    {
        // No need to update the health bar here
    }

    private void OnDestroy()
    {
        // Unsubscribe from the OnHealthChanged event to prevent memory leaks
        playerHealthScript.OnHealthChanged -= HandleHealthChanged;
    }
}