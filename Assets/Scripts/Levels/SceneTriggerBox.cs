using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTriggerBox : MonoBehaviour
{
    private SceneTriggerManager levelManager;
    public int targetSceneBuildIndex;
    public Vector3 playerTargetPosition; // The position where the player should be placed in the new scene

    private void Start()
    {
        levelManager = FindObjectOfType<SceneTriggerManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Save the player's target position to the SceneTriggerManager
            levelManager.SetPlayerTargetPosition(playerTargetPosition);
            LoadTargetScene();
        }
    }

    private void LoadTargetScene()
    {
        levelManager.LoadSceneFromTrigger(targetSceneBuildIndex);
    }
}