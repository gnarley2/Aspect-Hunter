using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] GameObject InventoryPanel;
    public static bool openInventory;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("i"))
        {
            if (!Menus.isPaused)
            {
                // Toggle the state of the inventory
                openInventory = !openInventory;
                InventoryPanel.SetActive(openInventory);
            }
        }

        // Optionally, you could also disable the inventory when the game is paused
        if (Menus.isPaused && openInventory)
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
