using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameManager gameManager;
    public Image xpBar;
    public Text coinText;

    private void Start()
    {
        if (gameManager == null)
        {
            Debug.LogError("GameManager is not assigned!");
        }
    }

    private void Update()
    {
        UpdateXPBar();
        UpdateCoinText();
    }

    private void UpdateXPBar()
    {
        // Get the current XP and the XP required for the next level
        int currentXP = gameManager.currentXP;
        int nextLevelXpRequired;
        if (gameManager.currentLevel < gameManager.maxLevels)
        {
            nextLevelXpRequired = gameManager.levelData[gameManager.currentLevel].xpRequired;
        }
        else
        {
            nextLevelXpRequired = 0; // No more levels to progress
        }

        // Update the XP bar fill amount based on the current XP and the XP required for the next level
        xpBar.fillAmount = (float)currentXP / nextLevelXpRequired;
    }

    private void UpdateCoinText()
    {
        // Update the coin text with the total coins collected
        coinText.text = gameManager.totalCoinsCollected.ToString();
    }
}