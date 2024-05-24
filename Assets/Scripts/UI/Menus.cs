using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Menus : MonoBehaviour
{
    TextMeshPro startButton;
    TextMeshPro instructionsButton;
    TextMeshPro quitButton;
    [SerializeField] GameObject MenuPanel;
    [SerializeField] GameObject InstructionPanel;
    
    public bool isPaused;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (MenuController.openInventory)
            {
                // If inventory is open, just close it and don't pause the game
                MenuController menuController = FindObjectOfType<MenuController>();
                if (menuController != null)
                {
                    menuController.CloseInventory();
                }
            }
            else if (MenuController.openBestiary)
            {
                MenuController menuController = FindObjectOfType<MenuController>();
                if (menuController != null)
                {
                    menuController.CloseBestiary();
                }
            }
            else
            {
                // Toggle pause state only if inventory is not open
                isPaused = !isPaused;

                if (isPaused)
                {
                    Time.timeScale = 0f;
                    GameCanvas.Instance.TogglePausedPanel(isPaused);
                }
                else
                {
                    Resume();
                }
            }
        }
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        isPaused = false;
        GameCanvas.Instance.TogglePausedPanel(isPaused);

        MenuController menuController = FindObjectOfType<MenuController>();
        if (menuController != null)
        {
            menuController.CloseInventory();
        }
    }

    public void Options()
    {
        GameCanvas.Instance.TogglePausedPanel(false);
        GameCanvas.Instance.ToggleOptionPanel(true);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(2);
    }

    public void Instructions()
    {
        MenuPanel.SetActive(false);
        InstructionPanel.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Back()
    {
        if (isPaused == true)
        {
            GameCanvas.Instance.ToggleOptionPanel(false);
            GameCanvas.Instance.TogglePausedPanel(true);
        }
        else
        {
            MenuPanel.SetActive(true);
            InstructionPanel.SetActive(false);
        }

    }

}
