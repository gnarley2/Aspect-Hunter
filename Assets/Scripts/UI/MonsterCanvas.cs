using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCanvas : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    private int lightCollisionCount = 0;

    private void Awake()
    {
        canvasGroup.alpha = 0f;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Light"))
        {
            lightCollisionCount++;
            canvasGroup.alpha = 1f;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Light"))
        {
            lightCollisionCount--;
            if (lightCollisionCount <= 0)
            {
                canvasGroup.alpha = 0f;
                lightCollisionCount = 0; // Ensure the count doesn't go below zero
            }
        }
    }
}