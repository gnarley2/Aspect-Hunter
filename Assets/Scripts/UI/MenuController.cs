using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField] Menus menus; // Assign this in the inspector or find it in Awake/Start
    public static bool openInventory;
    bool itemDescription = false;
    [SerializeField] GameObject DescriptionPanel;

    private void Awake()
    {
        menus = FindObjectOfType<Menus>(); // Only use this if Menus is not a Singleton
        openInventory = false;
    }

    void Update()
    {
        if (Input.GetKeyDown("i"))
        {
            if (!menus.isPaused) // Use the instance of Menus to check if it's paused
            {
                // Toggle the state of the inventory
                openInventory = !openInventory;
                GameCanvas.Instance.ToggleCharacterPanel(openInventory); //todo inventory
            }
        }

        if (menus.isPaused && openInventory) // Again, use the instance of Menus
        {
            // Close inventory when the game is paused
            openInventory = false;
            GameCanvas.Instance.ToggleCharacterPanel(openInventory);
        }

        if (itemDescription)
        {
            DescriptionPanel.SetActive(true);
        }

    }

    public void CloseInventory()
    {
        openInventory = false;
        GameCanvas.Instance.ToggleCharacterPanel(openInventory);
    }

    public void OpenDescription()
    {
        itemDescription = true;
    }
}
