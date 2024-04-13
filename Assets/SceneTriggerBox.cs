using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTriggerBox : MonoBehaviour
{
    private SceneTriggerManager levelManager;
    public SceneAsset targetScene;

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
        if (targetScene != null)
        {
            levelManager.LoadSceneFromTrigger(targetScene);
        }
        else
        {
            Debug.LogWarning("Target scene is not assigned.");
        }
    }
}
