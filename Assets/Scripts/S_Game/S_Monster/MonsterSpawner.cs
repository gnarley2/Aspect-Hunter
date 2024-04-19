using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private int maxNumber;
    [SerializeField] private float duration;
    [SerializeField] private Transform spawnPos;
    
    [SerializeField] private List<Monster> monsters;

    [SerializeField] private int currentNumber;

    private void Awake()
    {
        currentNumber = monsters.Count;
        InvokeRepeating("SpawnMonster", 0f, duration);
    }

    void MonsterDied()
    {
        InvokeRepeating("SpawnMonster", 0f, duration);
    }
    
    private void SpawnMonster()
    {
        Monster monster = Instantiate(prefab, spawnPos.position, Quaternion.identity).GetComponentInChildren<Monster>();
        monster.OnDied += MonsterDied;
        
        currentNumber++;
        if (currentNumber >= maxNumber)
        {
            CancelInvoke();
        }
        
        
    }
}
