using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEditor;

public class SceneTriggerManager : MonoBehaviour
{

    private string currentLevelName;

    private void Start()
    {
        currentLevelName = SceneManager.GetActiveScene().name;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Update()
    {

    }

    public void LoadSceneFromTrigger(SceneAsset targetScene)
    {
        if (targetScene != null)
        {
            
            SceneManager.LoadScene(targetScene.name);
        }
        else
        {
            Debug.LogWarning("Target scene is not assigned.");
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        string newLevelName = SceneManager.GetActiveScene().name;
        currentLevelName = newLevelName;
        Debug.Log($"Entered Level: {currentLevelName}");
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }


}
