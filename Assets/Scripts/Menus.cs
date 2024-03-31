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

    [SerializeField] GameObject PauseMenuUI;
    [SerializeField] GameObject OptionsUI;
    bool isPaused;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        isPaused = false;
        PauseMenuUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {   
            Time.timeScale = 0f;
            isPaused = true;
            PauseMenuUI.SetActive(true);
        }

        if (MenuPanel == null || InstructionPanel == null)
        {
            return;
        }        
        
        if (PauseMenuUI == null || OptionsUI == null)
        {
            return;
        }
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        isPaused = false;
        PauseMenuUI.SetActive(false);
    }

    public void Options()
    {
        OptionsUI.SetActive(true);
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
            OptionsUI.SetActive(false);
        else
        {
            MenuPanel.SetActive(true);
            InstructionPanel.SetActive(false);
        }

    }

}
