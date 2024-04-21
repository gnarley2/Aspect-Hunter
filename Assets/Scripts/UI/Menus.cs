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

    public void Resume()
    {
        Time.timeScale = 1f;
        isPaused = false;
        GameCanvas.Instance.TogglePausedPanel(isPaused);

        // FindItem the MenuController component and close the inventory
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
        SceneManager.LoadScene(1);
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
