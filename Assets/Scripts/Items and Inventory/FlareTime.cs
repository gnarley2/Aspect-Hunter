using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FlareTimer : MonoBehaviour
{
    // Time after which the GameObject should be destroyed
    public float destroyTime = 10f;

    // Duration over which to dim the light (in seconds)
    public float dimDuration = 2f;

    private Light2D backlight;
    private float initialIntensity;
    private float dimStartTime;

    void Start()
    {
        // Find the Light2D component attached to the child object called "backlight"
        backlight = transform.Find("Backlight").GetComponent<Light2D>();

        initialIntensity = backlight.intensity;
        dimStartTime = Time.time;

        // Call the DimLight method repeatedly until the light is completely dimmed
        InvokeRepeating("DimLight", 0f, 0.1f);

        // Call the DestroyObject method after 'destroyTime' seconds
        Invoke("DestroyObject", destroyTime);
    }

    void DimLight()
    {
        // Calculate the time elapsed since the dimming started
        float elapsedTime = Time.time - dimStartTime;

        // Calculate the current intensity based on the elapsed time and dim duration
        float currentIntensity = Mathf.Lerp(initialIntensity, 0f, elapsedTime / dimDuration);

        // Update the light intensity
        backlight.intensity = currentIntensity;

        // If the dimming is complete, cancel the repeating invocation
        if (elapsedTime >= dimDuration)
        {
            CancelInvoke("DimLight");
        }
    }

    void DestroyObject()
    {
        Destroy(gameObject);
    }
}
