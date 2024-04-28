using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDatabase : MonoBehaviour
{
    public static MonsterDatabase Instance;

    private void Awake()
    {
        Instance = this;
    }

    [Serializable]
    public class SingleMonster
    {
        public MonsterName name;
        public GameObject prefab;
    }

    [SerializeField] private List<SingleMonster> monsterList = new List<SingleMonster>();

    public GameObject GetMonsterPrefab(MonsterName name)
    {
        foreach (SingleMonster monster in monsterList)
        {
            if (monster.name == name)
            {
                return monster.prefab;
            }
        }
        
        Debug.LogError($"Can not find monster {name}");
        return null;
    }
}
