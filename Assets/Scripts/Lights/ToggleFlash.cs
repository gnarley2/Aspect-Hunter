using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class ToggleFlash : MonoBehaviour
{
    Light2D lightComponent;
    bool isFlashlightOn = false;
    float batteryLife = 100f; // Battery life in percentage
    float batteryDrainRate = 4f; // Battery drain rate per second
    float batteryRechargeRate = 3f; // Battery recharge rate per second
    float batteryMinIntensity = 0.2f; // Minimum light intensity when battery is low
    float batteryMaxIntensity = 1f; // Maximum light intensity when battery is full
    Slider batterySlider; // Reference to the UI Slider
    Image batteryFill; // Reference to the slider's fill image
    Color normalColor = Color.green; // Normal color
    Color lowBatteryColor = Color.red; // Color when battery is low
    float lowBatteryThreshold = 28f; // Battery percentage threshold to turn red
    float criticalBatteryThreshold = 16f; // Battery percentage threshold to start flashing
    bool isFlashing = false;

    void Start()
    {
        // Find the GameObject with the tag "BatterySlider" and get its Slider component
        GameObject sliderObject = GameObject.FindWithTag("BatterySlider");
        if (sliderObject != null)
        {
            batterySlider = sliderObject.GetComponent<Slider>();
            if (batterySlider == null)
            {
                Debug.LogError("No Slider component found on the GameObject with tag 'BatterySlider'");
            }
            else
            {
                Transform fillArea = sliderObject.transform.Find("Fill Area");
                if (fillArea == null)
                {
                    Debug.LogError("No 'Fill Area' child found on the GameObject with tag 'BatterySlider'");
                }
                else
                {
                    batteryFill = fillArea.Find("Fill").GetComponent<Image>();
                    if (batteryFill == null)
                    {
                        Debug.LogError("No 'Fill' Image component found on the GameObject with tag 'BatterySlider'");
                    }
                    else
                    {
                        batterySlider.maxValue = 100f; // Set the max value of the slider to 100
                        batterySlider.value = batteryLife; // Initialize the slider value
                        batteryFill.color = normalColor; // Set the initial color to normal
                    }
                }
            }
        }
        else
        {
            Debug.LogError("No GameObject found with tag 'BatterySlider'");
        }

        lightComponent = gameObject.GetComponent<Light2D>();
        if (lightComponent == null)
        {
            Debug.LogError("No Light2D component found on this GameObject");
        }
        else
        {
            lightComponent.intensity = 0; // Start with the flashlight off
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            ToggleFlashlight();
        }

        if (isFlashlightOn)
        {
            DrainBattery();
        }
        else
        {
            RechargeBattery();
        }

        AdjustLightIntensity();
        UpdateBatteryUI();
    }

    void ToggleFlashlight()
    {
        if (lightComponent == null)
        {
            Debug.LogError("Light2D component is null in ToggleFlashlight");
            return;
        }

        isFlashlightOn = !isFlashlightOn;
        lightComponent.intensity = isFlashlightOn ? batteryMaxIntensity : 0;

        if (!isFlashlightOn && isFlashing)
        {
            StopCoroutine(FlashBatteryUI());
            isFlashing = false;
            batteryFill.color = normalColor; // Reset the color when the flashlight is turned off
        }
    }

    void DrainBattery()
    {
        if (batteryLife > 0)
        {
            batteryLife -= batteryDrainRate * Time.deltaTime;
            batteryLife = Mathf.Clamp(batteryLife, 0, 100);
        }
        else
        {
            isFlashlightOn = false; // Turn off flashlight when battery is empty
            if (lightComponent != null)
            {
                lightComponent.intensity = 0;
            }
        }
    }

    void RechargeBattery()
    {
        if (batteryLife < 100)
        {
            batteryLife += batteryRechargeRate * Time.deltaTime;
            batteryLife = Mathf.Clamp(batteryLife, 0, 100);
        }
    }

    void AdjustLightIntensity()
    {
        if (lightComponent == null)
        {
            Debug.LogError("Light2D component is null in AdjustLightIntensity");
            return;
        }

        if (isFlashlightOn)
        {
            // Adjust the light intensity based on the remaining battery life
            float intensity = Mathf.Lerp(batteryMinIntensity, batteryMaxIntensity, batteryLife / 100);
            lightComponent.intensity = intensity;
        }
    }

    void UpdateBatteryUI()
    {
        if (batterySlider != null && batteryFill != null)
        {
            batterySlider.value = batteryLife; // Update the slider value to reflect the battery life

            if (isFlashlightOn && batteryLife <= criticalBatteryThreshold)
            {
                if (!isFlashing)
                {
                    StartCoroutine(FlashBatteryUI());
                }
            }
            else if (batteryLife <= lowBatteryThreshold)
            {
                batteryFill.color = lowBatteryColor; // Change the color to red
            }
            else
            {
                batteryFill.color = normalColor; // Change the color to normal
                isFlashing = false;
            }
        }
    }

    IEnumerator FlashBatteryUI()
    {
        isFlashing = true;
        while (isFlashlightOn && batteryLife <= criticalBatteryThreshold)
        {
            batteryFill.color = batteryFill.color == lowBatteryColor ? Color.clear : lowBatteryColor;
            yield return new WaitForSeconds(0.1f);
        }
        batteryFill.color = lowBatteryColor;
        isFlashing = false;
    }
}