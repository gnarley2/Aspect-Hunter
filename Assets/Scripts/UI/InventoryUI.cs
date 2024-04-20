using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour

{
    private static InventoryUI instance;
    [SerializeField] GameObject InventoryPanel;
    [SerializeField] Menus menus; // Assign this in the inspector or find it in Awake/Start
    public static bool openInventory;
    bool itemDescription = false;
    [SerializeField] GameObject DescriptionPanel;
    public static InventoryUI Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<InventoryUI>();
                if (instance == null)
                {
                    GameObject go = new GameObject("InventoryUI");
                    instance = go.AddComponent<InventoryUI>();
                }
            }

            return instance;
        }
    }

    private void Awake()
    {
        menus = FindObjectOfType<Menus>(); // Only use this if Menus is not a Singleton
        InventoryPanel.SetActive(false);
        openInventory = false;

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
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

        if (itemDescription)
        {
            DescriptionPanel.SetActive(true);
        }

    }

    public void CloseInventory()
    {
        InventoryPanel.SetActive(false);
        openInventory = false;
    }

    public void OpenDescription()
    {
        itemDescription = true;
    }
}
