using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField] Menus menus;
    public static bool openInventory;
    bool itemDescription = false;
    bool openMap;
    [SerializeField] GameObject DescriptionPanel;

    private void Awake()
    {
        menus = FindObjectOfType<Menus>();
        openInventory = false;
        openMap = false;
    }

    void Update()
    {
        if (Input.GetKeyDown("i"))
        {
            if (!menus.isPaused) // Check if the game is paused
            {
                ToggleInventory();
            }
        }

        if (Input.GetKeyDown("m"))
        {
            if (!menus.isPaused)
            {
                ToggleMap();
            }
        }
    }

    public void ToggleMap()
    {
        openMap = !openMap;
        GameCanvas.Instance.ToggleMapPanel(openMap);
    }

    public void ToggleInventory()
    {
        openInventory = !openInventory;
        GameCanvas.Instance.ToggleCharacterPanel(openInventory);
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