using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public GameObject instructionsPanel;
    public GameObject pausePanel;
    public GameObject creditsPanel;
    public Button pauseButton, reloadButton, menuButton;

    public PlayerMovement playerMovement;

    public void GoToMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void ShowInstructions()
    {
        instructionsPanel.SetActive(true);
    }

    public void HideInstructions()
    {
        instructionsPanel.SetActive(false);
    }

    public void ShowCredits()
    {
        creditsPanel.SetActive(true);
    }

    public void HideCredits()
    {
        creditsPanel.SetActive(false);
        
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        if (playerMovement != null) playerMovement.isPaused = true;
        else Debug.LogWarning("playerMovement script not assigned to button manager!");
        pausePanel.SetActive(true);
        pauseButton.interactable = false;
        menuButton.interactable = false;
        reloadButton.interactable = false;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        if (playerMovement != null) playerMovement.isPaused = false;
        else Debug.LogWarning("playerMovement script not assigned to button manager!");
        pausePanel.SetActive(false);
        pauseButton.interactable = true;
        menuButton.interactable = true;
        reloadButton.interactable = true;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }

    public void NextLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(0);
        }
        else
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
