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
    }

    private void Update()
    {
        string newLevelName = SceneManager.GetActiveScene().name;
        currentLevelName = newLevelName;
           
        
    }

    public void LoadSceneFromTrigger(SceneAsset targetScene)
    {
        if (targetScene != null)
        {
            Debug.Log("Loading level: " + targetScene.name);
            SceneManager.LoadScene(targetScene.name);
           
        }
        else
        {
            Debug.LogWarning("Target scene is not assigned.");
        }
    }

}
