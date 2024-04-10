using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GameSettings : MonoBehaviour
{
    public static GameSettings Instance;
    
     void Awake()
     {
         Instance = this;
     }


    [Header("Flash")] 
    public Material flashMat;
    public float flashCoolDown;

    [Header("Prefab")] 
    public GameObject enemyReleasedPrefab;


    public Action OnGameInitialized;

}
