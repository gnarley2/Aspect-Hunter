using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class HologramTutorial : MonoBehaviour
{
    public float hologramdestroyDelay = 2f;
    [SerializeField] private SpriteRenderer[] sprites;
    [SerializeField] private TextMeshPro[] texts;
    [SerializeField] private float duration = 0.5f;
    [SerializeField] private bool startOnPlay = false;

    [SerializeField] private GameObject hologramPrefab; // Field for hologram prefab
    [SerializeField] private Vector2 hologramOffset;  // Offset for hologram position

    private GameObject instantiatedHologram;  // Reference to the instantiated hologram
    private Coroutine destroyCoroutine;  // Reference to the destroy coroutine
    private Coroutine checkHologramCoroutine;  // Reference to the check hologram coroutine

    private void Awake()
    {
        ToggleTutorial(startOnPlay);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ToggleTutorial(true);
            // Start the check hologram coroutine
            if (checkHologramCoroutine == null)
            {
                checkHologramCoroutine = StartCoroutine(CheckHologramRoutine());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ToggleTutorial(false);
            if (instantiatedHologram != null)
            {
                Animator hologramAnim = instantiatedHologram.GetComponent<Animator>();
                if (hologramAnim != null)
                {
                    hologramAnim.SetTrigger("End");
                    // Start the destroy coroutine if it's not already running
                    if (destroyCoroutine == null)
                    {
                        destroyCoroutine = StartCoroutine(DestroyAfterTime(hologramdestroyDelay));
                    }
                }
            }
            // Stop the check hologram coroutine
            if (checkHologramCoroutine != null)
            {
                StopCoroutine(checkHologramCoroutine);
                checkHologramCoroutine = null;
            }
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

    void InstantiateHologram(Vector3 basePosition)
    {
        if (hologramPrefab != null)
        {
            Vector3 instantiatePosition = basePosition + (Vector3)hologramOffset;
            instantiatedHologram = Instantiate(hologramPrefab, instantiatePosition, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Hologram prefab is not assigned.");
        }
    }

    IEnumerator DestroyAfterTime(float delay)
    {
        yield return new WaitForSeconds(delay);

        Destroy(instantiatedHologram);
        instantiatedHologram = null;
        destroyCoroutine = null;  // Reset the destroy coroutine reference
    }

    IEnumerator CheckHologramRoutine()
    {
        while (true)
        {
            if (instantiatedHologram == null)
            {
                InstantiateHologram(transform.position);
            }
            yield return new WaitForSeconds(0.5f);  // Adjust the frequency of the check as needed
        }
    }
}