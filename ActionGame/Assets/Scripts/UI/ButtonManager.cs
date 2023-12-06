using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public GameObject instructionsPanel;

    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void GoToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void ShowInstructions(){
        instructionsPanel.SetActive(true);
    }

    public void HideInstructions(){
        instructionsPanel.SetActive(false);
    }

    public void PauseGame()
    {

    }

    public void ResumeGame()
    {

    }

    public void RestartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
