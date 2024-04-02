using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] GameObject InventoryPanel;
    [SerializeField] Menus menus; // Assign this in the inspector or find it in Awake/Start
    public static bool openInventory;

    private void Awake()
    {
        menus = FindObjectOfType<Menus>(); // Only use this if Menus is not a Singleton
    }

    void Update()
    {
        if (Input.GetKeyDown("i"))
        {
            if (!menus.isPaused) // Use the instance of Menus to check if it's paused
            {
                // Toggle the state of the inventory
                openInventory = !openInventory;
                InventoryPanel.SetActive(openInventory);
            }
        }

        if (menus.isPaused && openInventory) // Again, use the instance of Menus
        {
            // Close inventory when the game is paused
            InventoryPanel.SetActive(false);
            openInventory = false;
        }
    }

    public void CloseInventory()
    {
        InventoryPanel.SetActive(false);
        openInventory = false;
    }
}
