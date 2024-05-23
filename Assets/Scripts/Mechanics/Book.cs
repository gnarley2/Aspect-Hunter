using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Book : MonoBehaviour
{
    [SerializeField] private string id;
    [TextArea(5, 20)] public string text;
    [SerializeField] private TextMeshPro[] texts;
    [SerializeField] private float duration = 0.5f;
    
    
    private bool isInRange = false;

    private void OnValidate()
    {
        if (id == String.Empty) id = System.Guid.NewGuid().ToString();
    }

    private void Update()
    {
        if (!isInRange) return;
        if (Input.GetKeyDown(KeyCode.F))
        {
            Pickup();
        }
    }

    private void Pickup()
    {
        BookUI.Instance.SetText(text);
        BookUI.Instance.Toggle(true);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = true;
            ToggleSign(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = false;
            ToggleSign(false);
        }
    }
    
    void ToggleSign(bool isEnable)
    {
        float targetAlpha = isEnable ? 1 : 0;

        foreach (TextMeshPro text in texts)
        {
            StartCoroutine(ToggleCoroutine(text, targetAlpha));
        }
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
