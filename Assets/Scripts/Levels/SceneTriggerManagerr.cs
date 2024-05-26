using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class SceneTriggerManager : MonoBehaviour
{
    //for scene transitions
    [SerializeField] Animator transistionAnim;
 
    private string currentLevelName;
    private Vector3 playerTargetPosition; // The position to move the player to in the new scene
    private int savedProjectileIndex = -1;
    public Health health;
    private int tsceneIndex;
    private void Start()
    {
        DontDestroyOnLoad(this);
        currentLevelName = SceneManager.GetActiveScene().name;
        SceneManager.sceneLoaded += OnSceneLoaded;
        health = GetComponentInChildren<Health>();

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
            tsceneIndex=targetSceneIndex;
          //  transistionAnim.SetTrigger("End");
            StartCoroutine(SceneTransition());
            SavePlayerState(); // Save the player's state before loading the new scene
           
        }
        else
        {
            Debug.LogWarning("Invalid target scene index.");
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
       // transistionAnim.SetTrigger("Start");
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

    IEnumerator SceneTransition()
    {
        transistionAnim.SetTrigger("End");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(tsceneIndex);
        transistionAnim.SetTrigger("Start");
        health.SetHealthToMax();
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