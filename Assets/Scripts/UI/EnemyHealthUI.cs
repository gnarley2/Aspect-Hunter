using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthUI : MonoBehaviour
{
    [SerializeField] private Health health;
    
    private Slider slider;
    [SerializeField] private Image fillImg;

    private Color fullColor = Color.green;
    private Color noColor = Color.red;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    private void OnEnable()
    {
        health.OnUpdateHealth += UpdateHealthUI;
    }

    private void OnDisable()
    {
        health.OnUpdateHealth -= UpdateHealthUI;
    }
    

    private void UpdateHealthUI(int obj)
    {
        slider.value = health.GetPercent();
        Debug.Log(slider.value);
        fillImg.color = Color.Lerp(noColor, fullColor, slider.value);
    }
}
