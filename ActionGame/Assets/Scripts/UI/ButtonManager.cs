using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public GameObject instructionsPanel;
    public GameObject pausePanel;
    public Button pauseButton;

    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void GoToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void ShowInstructions()
    {
        instructionsPanel.SetActive(true);
    }

    public void HideInstructions()
    {
        instructionsPanel.SetActive(false);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
        pauseButton.interactable = false;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
        pauseButton.interactable = true;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("level_1");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
