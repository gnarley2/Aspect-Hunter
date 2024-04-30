using System;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Data/ProjectileData")]
public class ProjectileData : ScriptableObject
{

    public string name;
    public AspectType type;
    public GameObject prefab;
    public bool isUnlocked = false;

    public void Unlock(MonsterData data)
    {
        if (isUnlocked) return;

        if (String.Equals(data.monsterDetails.type.ToString(), type.ToString()))
        {
            InformationPanel.Instance.ShowInformation($"{name} is unlocked");
            isUnlocked = true;
        }
    }
}
