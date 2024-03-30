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
        MenuPanel.SetActive(true);
        InstructionPanel.SetActive(false);
    }

}
