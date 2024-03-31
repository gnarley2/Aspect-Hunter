using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "ScriptableObjects/Data/Player/HitData", fileName = "HitData")]
public class HitData : ScriptableObject
{
    [Header("Hit")]
    public float invulnerableTime;

    public float resetGroundPlayerPositionTime;
    
    [Space(5)]
    [Range(0, 1f)] public float hitSleepTime; // Feel more impact
    [Range(0, 1f)] public float hitResetSleepTime; // Feel more impact

    [Header("VFX")] public PooledObjectData[] vfx;
    
    [Header("Sound")] public AudioClip clip;

    [Header("Cam Shake")] public CamShakeData camShakeData;
}
