using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager Instance;
    private SceneTriggerManager sceneTriggerManager;

    public Vector2 playerCheckPoint;
    public int sceneIndex;
    public bool saveOnStart;

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

        sceneTriggerManager = GetComponentInChildren<SceneTriggerManager>();
    }

    private void Start()
    {
        if (saveOnStart)
        {
            sceneIndex = SceneManager.GetActiveScene().buildIndex;
            playerCheckPoint = GameObject.FindWithTag("Player").transform.position;
        }
    }

    public void LoadLastCheckpoint()
    {
        sceneTriggerManager.SetPlayerTargetPosition(playerCheckPoint);
        sceneTriggerManager.LoadSceneFromTrigger(sceneIndex);
    }
    
    public void SetCheckpoint(Vector2 newCheckpoint)
    {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        playerCheckPoint = newCheckpoint;
    }

}
