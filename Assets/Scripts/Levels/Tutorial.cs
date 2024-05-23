using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private SpriteRenderer[] sprites;
    [SerializeField] private TextMeshPro[] texts;
    [SerializeField] private float duration = 0.5f;
    [SerializeField] private bool startOnPlay = false;

    private void Awake()
    {
        ToggleTutorial(startOnPlay);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ToggleTutorial(true);            
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ToggleTutorial(false);            
        }
    }

    void ToggleTutorial(bool isEnable)
    {
        float targetAlpha = isEnable ? 1 : 0;
        foreach (SpriteRenderer sprite in sprites)
        {
            StartCoroutine(ToggleCoroutine(sprite, targetAlpha));
        }
        foreach (TextMeshPro text in texts)
        {
            StartCoroutine(ToggleCoroutine(text, targetAlpha));
        }
    }

    IEnumerator ToggleCoroutine(SpriteRenderer sprite, float targetAlpha)
    {
        float time = Time.time;
        Color startColor = sprite.color;
        while (time + duration > Time.time)
        {
            float alpha = Mathf.Lerp(startColor.a, targetAlpha, (Time.time - time) / duration);
            sprite.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
            yield return null;
        }
        
        sprite.color = new Color(startColor.r, startColor.g, startColor.b, targetAlpha);
    }
    
    IEnumerator ToggleCoroutine(TextMeshPro text, float targetAlpha)
    {
        float time = Time.time;
        Color startColor = text.color;
        while (time + duration > Time.time)
        {
            float alpha = Mathf.Lerp(startColor.a, targetAlpha, (Time.time - time) / duration);
            text.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
            yield return null;
        }
        
        text.color = new Color(startColor.r, startColor.g, startColor.b, targetAlpha);
    }
}
