using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class KeyManager : MonoBehaviour
{
    [SerializeField] private int numDoorsInLevel = 1; 
    private int numDoorsClosed = 0;

    private int numKeys = 0;
    public TextMeshProUGUI keyCounter;

    void Start()
    {   
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

        if (other.CompareTag("Door") && numKeys > 0)
        {
            other.gameObject.GetComponent<EnemySpawner>().isSpawning = false;
            numKeys--;
            numDoorsClosed++;
            if (numDoorsClosed == numDoorsInLevel) GameOver();
        }

        UpdateGUI();
    }

    private void UpdateGUI() {
        keyCounter.SetText("X" + numKeys); 
    }

    private void GameOver() {
        SceneManager.LoadScene("next_level");
    }
}
