using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;


public class tutorialsignpost : MonoBehaviour
{
    public string message;
    public GameObject messageWindowPrefab; // Reference to the message window prefab
    private GameObject messageWindowInstance;
    private TextMeshProUGUI messageText;

    private bool isPlayerInRange = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            // Hide message prompt
            HideMessageWindow();
        }
    }

    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            ToggleMessageWindow();
        }
    }

    private void ToggleMessageWindow()
    {
        if (messageWindowInstance == null)
        {
            ShowMessageWindow();
        }
        else
        {
            HideMessageWindow();
        }
    }

    private void ShowMessageWindow()
    {
        if (messageWindowInstance == null && messageWindowPrefab != null)
        {
            Vector3 offset = new Vector3(0, 1, 0);
            messageWindowInstance = Instantiate(messageWindowPrefab, transform.position + offset, Quaternion.identity);
            // messageWindowInstance.transform.SetParent(transform.parent, false);
            messageWindowInstance.transform.SetParent(GameObject.Find("SignPost Canvas").transform, false);
            // Find the TextMeshPro component in the instantiated message window
           // messageText = messageWindowInstance.GetComponentInChildren<TextMeshProUGUI>();
           // if (messageText != null)
           // {
                // Update the text
           //     messageText.text = message;
           // }
        }
    }

    private void HideMessageWindow()
    {
        Destroy(messageWindowInstance);
    }
}