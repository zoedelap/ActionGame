using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KeyManager : MonoBehaviour
{
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
            print("level complete");
        }

        UpdateGUI();
    }

    private void UpdateGUI() {
        keyCounter.SetText("X" + numKeys); 
    }
}
