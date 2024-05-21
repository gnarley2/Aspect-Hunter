using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class SaveLoadManager : MonoBehaviour
{
    public static SaveLoadManager Instance;

    public Vector2 playerCheckPoint;
    public int sceneIndex;
    public List<string> keys = new List<string>();
    public List<string> BookList = new List<string>();

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

    private void Start()
    {
        SceneManager.sceneLoaded += ResetOnSceneLoad;
    }

    private void ResetOnSceneLoad(Scene scene, LoadSceneMode arg1)
    {
        if (sceneIndex != scene.buildIndex)
        {
            sceneIndex = scene.buildIndex;
            playerCheckPoint = GameObject.FindWithTag("Player").transform.position;
            keys = new List<string>();
        }
        else
        {
            GameObject.FindWithTag("Player").transform.position = playerCheckPoint;
        }

        if (HasEnoughBook())
        {
            GameObject hiddenGO = GameObject.FindWithTag("HiddenLevel");
            if (hiddenGO)
            {
                CanvasGroup canvasGroup = hiddenGO.GetComponent<CanvasGroup>();
                canvasGroup.alpha = 1;
                canvasGroup.interactable = true;
                canvasGroup.blocksRaycasts = true;
            }
        }
    }
    
    public void SetCheckpoint(Vector2 newCheckpoint)
    {
        playerCheckPoint = newCheckpoint;
    }

    public void AddKey(string id)
    {
        if (HasKey(id)) return;
        
        keys.Add(id);
    }

    public bool HasKey(string id)
    {
        return keys.IndexOf(id) != -1;
    }

    public int GetKeyCount()
    {
        return keys.Count;
    }

    public void AddBook(string id)
    {
        if (HasBook(id)) return;
        
        BookList.Add(id);
    }
    
    public bool HasBook(string id)
    {
        if (BookList.Contains(id)) return true;
        return false;
    }

    public bool HasEnoughBook()
    {
        Debug.Log(BookList.Count);
        return BookList.Count >= 3;
    }
}
