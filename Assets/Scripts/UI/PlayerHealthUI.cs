using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    [SerializeField] private Image fillImg;

    private Slider slider;
    private Health health;

    private Color fullColor = Color.green;
    private Color noColor = Color.red;

    private void Awake()
    {
        slider = GetComponent<Slider>();
        health = GameObject.FindWithTag("Player").GetComponentInChildren<Health>();
    }

    private void OnEnable()
    {
        
        health.OnUpdateHealth += UpdateHealthUI;
    }
    

    private void OnDisable()
    {
        health.OnUpdateHealth -= UpdateHealthUI;
    }

    private void UpdateHealthUI(int value)
    {
        slider.value = health.GetPercent();
        fillImg.color = Color.Lerp(noColor, fullColor, slider.value);
    }
}
