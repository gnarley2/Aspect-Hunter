using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCanvas : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup.alpha = 0f;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canvasGroup.alpha = 1f;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canvasGroup.alpha = 0f;
        }
    }
}
