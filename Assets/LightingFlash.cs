using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightingFlash : MonoBehaviour
{
    
    public Light2D lightComponent; // Reference to the Light2D component
    public float pulseMagnitude = 1.0f; // The magnitude of the pulse
    public float pulseSpeed = 1.0f; // The speed of the pulse
    public float pulseDelay = 1.0f; // Delay before the pulse starts

    private float baseIntensity; // The base intensity of the light
    private float elapsedTime; // Time elapsed since the Start method
    private bool hasFlashed; // Flag to track if the flash has occurred

    void Start()
    {
        // Set the base intensity to 0
        baseIntensity = 0f;
        lightComponent.intensity = baseIntensity;

        hasFlashed = false;
        elapsedTime = 0f;
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;

        if (!hasFlashed && elapsedTime >= pulseDelay)
        {
            // Calculate the pulse factor based on time
            float pulseFactor = (Mathf.Sin((elapsedTime - pulseDelay) * pulseSpeed) + 1f) * 0.5f * pulseMagnitude;

            // Update the light intensity based on the pulse factor
            lightComponent.intensity = pulseFactor;

            // If the pulse factor is back to 0, set the flag to prevent further flashing
            if (pulseFactor == 0f)
            {
                hasFlashed = true;
            }
        }
    }
}