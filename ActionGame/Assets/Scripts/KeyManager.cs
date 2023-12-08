using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class KeyManager : MonoBehaviour
{
    private int numDoorsInLevel = 1; 
    private int numDoorsClosed = 0;

    private int numKeys = 0;
    public TextMeshProUGUI keyCounter;
    public GameObject nextLevelPanel; // in the future this should be moved to a designated game state manager and include the gameOverPanel object seen in Health.cs
    void Start()
    {
        GameObject[] doors = GameObject.FindGameObjectsWithTag("Door");
        numDoorsInLevel = doors.Length;
        Debug.Log(numDoorsInLevel + " doors in level");
        UpdateGUI();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Key"))
        {
            print("entered key");
            numKeys++;
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Door"))
        {
            bool doorIsSpawning = other.GetComponent<EnemySpawner>().isSpawning;
            if (doorIsSpawning && numKeys > 0)
            {
                other.gameObject.GetComponent<EnemySpawner>().isSpawning = false;
                numKeys--;
                numDoorsClosed++;
                if (numDoorsClosed == numDoorsInLevel) GameOver();
            }
        }
        else
        {
            // return here so we only update the GUI if it actually needs to be updated
            return;
        }

        UpdateGUI();
    }

    private void UpdateGUI()
    {
        Debug.Log("updated number of keys on screen");
        keyCounter.SetText("X" + numKeys);
    }

    private void GameOver()
    {
        Time.timeScale = 0;
        nextLevelPanel.SetActive(true);
    }
}
