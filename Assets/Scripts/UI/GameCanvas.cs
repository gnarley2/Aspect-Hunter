using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCanvas : MonoBehaviour
{
    public static GameCanvas Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    [SerializeField] private CanvasGroup HUD;
    [SerializeField] private CanvasGroup PausedPanel;
    [SerializeField] private CanvasGroup OptionsPanel;
    [SerializeField] private CanvasGroup CharacterPanel;
    [SerializeField] private float lerpDuration = 0.3f;

    public void ToggleHUD(bool active)
    {
        float startAlpha = active ? 0 : 1;
        StartCoroutine(ToggleCoroutine(HUD, startAlpha, 1 - startAlpha));
    }
    
    public void TogglePausedPanel(bool active)
    {
        float startAlpha = active ? 0 : 1;
        StartCoroutine(ToggleCoroutine(PausedPanel, startAlpha, 1 - startAlpha));
    }
    
    public void ToggleOptionPanel(bool active)
    {
        float startAlpha = active ? 0 : 1;
        StartCoroutine(ToggleCoroutine(OptionsPanel, startAlpha, 1 - startAlpha));
    }
    
    public void ToggleCharacterPanel(bool active)
    {
        float startAlpha = active ? 0 : 1;
        StartCoroutine(ToggleCoroutine(CharacterPanel, startAlpha, 1 - startAlpha));
    }
    
    IEnumerator ToggleCoroutine(CanvasGroup canvasGroup, float startAlpha, float targetAlpha)
    {
        if (canvasGroup.alpha == targetAlpha) yield break;
        
        float time = Time.realtimeSinceStartup;
        while (time + lerpDuration > Time.realtimeSinceStartup)
        {
            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, (Time.realtimeSinceStartup - time) / lerpDuration);
            yield return new WaitForSecondsRealtime(0.01f);
        }

        if (targetAlpha == 1)
        {
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }
        else
        {
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }
        canvasGroup.alpha = targetAlpha;
    }
    
}
