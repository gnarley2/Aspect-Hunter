using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
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
        fillImg.color = Color.Lerp(noColor, fullColor, slider.value);
        Debug.Log(slider.value);
    }
}
