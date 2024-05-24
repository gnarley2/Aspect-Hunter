using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class ToggleFlash : MonoBehaviour
{
    Light2D lightComponent;
    bool isFlashlightOn = false;
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
    BatteryManager batteryManager;

    void Start()
    {
        batteryManager = BatteryManager.instance;

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
                        batterySlider.maxValue = batteryManager.maxBattery; // Set the max value of the slider to max battery
                        batterySlider.value = batteryManager.currentBattery; // Initialize the slider value
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
            batteryManager.DrainBattery(batteryDrainRate);
        }
        else
        {
            batteryManager.RechargeBattery(batteryRechargeRate);
        }

        AdjustLightIntensity();
        UpdateBatteryUI();
    }

    public void TurnOffFlashlight()
    {
        isFlashlightOn = false;
        batteryManager.RechargeBattery(batteryRechargeRate);
        if (lightComponent != null)
        {
            lightComponent.intensity = 0;
        }
        if (isFlashing)
        {
            StopCoroutine(FlashBatteryUI());
            isFlashing = false;
            if (batteryFill != null)
            {
                batteryFill.color = normalColor; // Reset the color when the flashlight is turned off
            }
        }
    }

    void ToggleFlashlight()
    {
        if (lightComponent == null)
        {
            Debug.LogError("Light2D component is null in ToggleFlashlight");
            return;
        }

        if (batteryManager.IsBatteryEmpty())
        {
            isFlashlightOn = false;
            lightComponent.intensity = 0;
        }
        else
        {
            isFlashlightOn = !isFlashlightOn;
            lightComponent.intensity = isFlashlightOn ? batteryMaxIntensity : 0;

            if (!isFlashlightOn && isFlashing)
            {
                StopCoroutine(FlashBatteryUI());
                isFlashing = false;
                if (batteryFill != null)
                {
                    batteryFill.color = normalColor; // Reset the color when the flashlight is turned off
                }
            }
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
            float intensity = Mathf.Lerp(batteryMinIntensity, batteryMaxIntensity, batteryManager.currentBattery / batteryManager.maxBattery);
            lightComponent.intensity = intensity;
        }
    }

    void UpdateBatteryUI()
    {
        if (batterySlider != null && batteryFill != null)
        {
            batterySlider.value = batteryManager.currentBattery; // Update the slider value to reflect the battery life

            if (isFlashlightOn && batteryManager.currentBattery <= criticalBatteryThreshold)
            {
                if (!isFlashing)
                {
                    StartCoroutine(FlashBatteryUI());
                }
            }
            else if (batteryManager.currentBattery <= lowBatteryThreshold)
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
        while (isFlashlightOn && batteryManager.currentBattery <= criticalBatteryThreshold)
        {
            if (batteryFill != null)
            {
                batteryFill.color = batteryFill.color == lowBatteryColor ? Color.clear : lowBatteryColor;
            }
            yield return new WaitForSeconds(0.1f);
        }
        if (batteryFill != null)
        {
            batteryFill.color = lowBatteryColor;
        }
        isFlashing = false;
    }
}
