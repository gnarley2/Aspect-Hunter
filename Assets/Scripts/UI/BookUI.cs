using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BookUI : MonoBehaviour
{
    public static BookUI Instance { get; private set; }
    private TextMeshProUGUI text;
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }
        
        Instance = this;
        text = GetComponentInChildren<TextMeshProUGUI>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void Toggle(bool isActive)
    {
        if (isActive) StartCoroutine(FadeIn());
        else StartCoroutine(FadeOut());
    }
    
    public IEnumerator FadeIn()
    {
        yield return Fade(1, 1);
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    public IEnumerator FadeOut()
    {
        yield return Fade(0, 1);
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    IEnumerator Fade(float targetAlpha, float duration)
    {
        float startAlpha = canvasGroup.alpha;
        float time = 0;
        while (time < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = targetAlpha;    
    }

    public void SetText(string text)
    {
        this.text.SetText(text);
    }
}
