using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MonsterInventory : MonoBehaviour
{
    [Serializable]
    public class SingleMonsterInventory
    {
        public MonsterData monsterData;
        public bool isInInventory = true;
    }
    
    public static MonsterInventory Instance;
    public List<SingleMonsterInventory> Monsters = new List<SingleMonsterInventory>();
    public Action<MonsterData> OnCatchMonster;
    
    private void Awake()
    {
        Instance = this;
    }

    
    #region Monster

    public void AddMonster(MonsterData data)
    {
        SingleMonsterInventory monsterInventory = new SingleMonsterInventory();
        monsterInventory.monsterData = data;
        
        Monsters.Add(monsterInventory);
        
        OnCatchMonster?.Invoke(data);
    }

    public void RemoveMonster(int index)
    {
        Monsters.RemoveAt(index);
    }
    
    public bool HasMonster(int index)
    {
        if (index < 0 || index >= Monsters.Count) return false;

        return true;
    }

    public MonsterData GetMonster(int index)
    {
        if (index < 0 || index >= Monsters.Count) return null;

        return Monsters[index].monsterData;
    }

    public int GetMonsterIndex(MonsterName name)
    {
        for (int i = 0; i < Monsters.Count; i++)
        {
            if (Monsters[i].monsterData.monsterDetails.name == name)
            {
                return i;
            }
        }

        Debug.LogError("Can't find this monster index");
        return -1;
    }

    public GameObject ReleaseMonster(MonsterName name)
    {
        return ReleaseMonster(GetMonsterIndex(name));
    }

    public GameObject ReleaseMonster(int index)
    {
        if (index < 0 || index >= Monsters.Count)
        {
            Debug.LogError("Out of index monster inventory");
            return null;
        }

        if (Monsters[index].isInInventory)
        {
            Monsters[index].isInInventory = false;

            MonsterData data = Monsters[index].monsterData;
            
            GameObject prefab = Instantiate(MonsterDatabase.Instance.GetMonsterPrefab(data.monsterDetails.name), transform.position, Quaternion.identity);
            prefab.GetComponentInChildren<Monster>().InitializeUponReleasing(data, index);
            
            return prefab;
        }
        else
        {
            Debug.LogError("Monster have been released");
        }
        
        return null;
    }
    
    public void UnReleaseMonster(Monster monster, int index)
    {
        if (index < 0 || index >= Monsters.Count)
        {
            Debug.LogError("Out of index monster inventory");
            return ;
        }

        if (!Monsters[index].isInInventory)
        {
            Monsters[index].isInInventory = true;
            monster.Destroy();
        }
        else
        {
            Debug.LogError("Monster have been unreleased");
        }
    }

    #endregion
}
