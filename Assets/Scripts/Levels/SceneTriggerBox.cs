using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTriggerBox : MonoBehaviour
{

    private SceneTriggerManager levelManager;
    public int targetSceneBuildIndex; // Use int to represent scene build index instead of SceneAsset

    private void Start()
    {
        levelManager = FindObjectOfType<SceneTriggerManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            LoadTargetScene();
        }
    }

    private void LoadTargetScene()
    {
        levelManager.LoadSceneFromTrigger(targetSceneBuildIndex);
    }
}
