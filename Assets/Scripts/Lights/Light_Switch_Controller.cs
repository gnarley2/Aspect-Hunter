using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Light_Switch_Controller : MonoBehaviour
{
    public Light2D[] lights;
    private bool lightsOn = false; 

    private void Start()
    {
        lights = GetComponentsInChildren<Light2D>(true);
        foreach (Light2D light in lights)
        {
            light.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Fire_Projectile(Clone)")
        {
            ToggleLights();
        }
    }

    private void ToggleLights()
    {
        lightsOn = !lightsOn;

        foreach (Light2D light in lights)
        {
            light.enabled = lightsOn;
        }
    }
}
