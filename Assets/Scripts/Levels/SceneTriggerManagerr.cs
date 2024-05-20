using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEditor;

public class SceneTriggerManager : MonoBehaviour
{
   
    private string currentLevelName;
    private Vector3 playerTargetPosition; // The position to move the player to in the new scene
    private int savedProjectileIndex = -1;

    private void Start()
    {
        currentLevelName = SceneManager.GetActiveScene().name;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void SetPlayerTargetPosition(Vector3 position)
    {
        playerTargetPosition = position;
    }

    public void SetProjectileIndex(int index)
    {
        savedProjectileIndex = index;
    }

    public void LoadSceneFromTrigger(int targetSceneIndex)
    {
        if (targetSceneIndex >= 0 && targetSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SavePlayerState(); // Save the player's state before loading the new scene
            SceneManager.LoadScene(targetSceneIndex);
        }
        else
        {
            Debug.LogWarning("Invalid target scene index.");
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        currentLevelName = scene.name;
        Debug.Log($"Entered Level: {currentLevelName}");

        // Find the player in the new scene and set its position
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.transform.position = playerTargetPosition;

            // Restore the projectile index
            PlayerCombat playerCombat = player.GetComponent<PlayerCombat>();
            if (playerCombat != null)
            {
                playerCombat.SetProjectileIndex(savedProjectileIndex);
            }
        }
    }

    private void SavePlayerState()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            PlayerCombat playerCombat = player.GetComponent<PlayerCombat>();
            if (playerCombat != null)
            {
                savedProjectileIndex = playerCombat.GetCurrentProjectileIndex();
            }
        }
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}