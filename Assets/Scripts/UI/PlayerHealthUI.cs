using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        FindPlayerHealth();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        health.OnUpdateHealth += UpdateHealthUI;
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        FindPlayerHealth();
    }

    void FindPlayerHealth()
    {
        health = GameObject.FindWithTag("Player").GetComponentInChildren<Health>();
        UpdateHealthUI(0);
    }


    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        health.OnUpdateHealth -= UpdateHealthUI;
    }

    private void UpdateHealthUI(int value)
    {
        slider.value = health.GetPercent();
        fillImg.color = Color.Lerp(noColor, fullColor, slider.value);
    }
}
