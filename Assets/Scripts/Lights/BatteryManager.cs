using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryManager : MonoBehaviour
{
    public float maxBattery = 100f;
    public float currentBattery;
    public static BatteryManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        currentBattery = maxBattery;
    }

    public void DrainBattery(float amount)
    {
        currentBattery -= amount * Time.deltaTime;
        currentBattery = Mathf.Clamp(currentBattery, 0, maxBattery);
    }

    public void RechargeBattery(float amount)
    {
        currentBattery += amount * Time.deltaTime;
        currentBattery = Mathf.Clamp(currentBattery, 0, maxBattery);
    }

    public bool IsBatteryEmpty()
    {
        return currentBattery <= 0;
    }
}