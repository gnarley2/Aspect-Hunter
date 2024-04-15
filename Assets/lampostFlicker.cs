using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class lampostFlicker : MonoBehaviour
{
    public Light2D lightComponent; // Reference to the Light2D component
    public float offDuration = 1f; // Duration in seconds the light stays off
    public float onDuration = 5f; // Duration in seconds the light stays on
    public bool startOn = true; // Whether the light should start in the on or off state

    private bool isLightOn; // Flag to track the current state of the light
    private float timeRemaining; // Time remaining in the current on/off cycle

    void Start()
    {
        // Get the Light2D component attached to the same GameObject
        if (lightComponent == null)
            lightComponent = GetComponent<Light2D>();

        // Set the initial state of the light
        isLightOn = startOn;
        lightComponent.enabled = isLightOn;

        // Set the initial time remaining based on the start state
        timeRemaining = isLightOn ? onDuration : offDuration;
    }

    void Update()
    {
        // Decrement the time remaining
        timeRemaining -= Time.deltaTime;

        // If the time remaining has elapsed, toggle the light state
        if (timeRemaining <= 0)
        {
            isLightOn = !isLightOn;
            lightComponent.enabled = isLightOn;

            // Set the new time remaining based on the new state
            timeRemaining = isLightOn ? onDuration : offDuration;
        }
    }
}