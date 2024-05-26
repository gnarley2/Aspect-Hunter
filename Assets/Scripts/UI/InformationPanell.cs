using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class InformationPanel : MonoBehaviour
{
    [SerializeField] private float lerpTime = 0.5f;
    [SerializeField] private TextMeshProUGUI textMeshPro;
    [SerializeField] private CanvasGroup canvasGroup;
    [FormerlySerializedAs("duration")] [SerializeField] private float lastDuration = 2f;
    
    public static InformationPanel Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        MonsterInventory.Instance.OnCatchMonster += ShowMonsterInformation;
    }

    private void ShowMonsterInformation(MonsterData data)
    {
        ShowInformation($"Successfully catch {data.name}");
    }

    public void ShowInformation(string text)
    {
        StopAllCoroutines();
        canvasGroup.alpha = 0f;
        
        textMeshPro.SetText(text);
        StartCoroutine(ShowInformationCoroutine());
    }

    IEnumerator ShowInformationCoroutine()
    {
        float time = Time.time;
        while (time + lerpTime > Time.time)
        {
            canvasGroup.alpha = Mathf.Lerp(0, 1, (Time.time - time) / lerpTime);
            yield return null;
        }

        canvasGroup.alpha = 1f;
        
        yield return new WaitForSeconds(lastDuration);
        
        time = Time.time;
        while (time + lerpTime > Time.time)
        {
            canvasGroup.alpha = Mathf.Lerp(1, 0, (Time.time - time) / lerpTime);
            yield return null;
        }

        canvasGroup.alpha = 0f;
    }
}
