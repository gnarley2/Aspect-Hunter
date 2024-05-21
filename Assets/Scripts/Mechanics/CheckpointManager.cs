using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager Instance;

    public Vector2 playerCheckPoint;
    public int sceneIndex;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        playerCheckPoint = GameObject.FindWithTag("Player").transform.position;
    }

    public void LoadLastCheckpoint()
    {
        
    }
    
    public void SetCheckpoint(Vector2 newCheckpoint)
    {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        playerCheckPoint = newCheckpoint;
    }

}
