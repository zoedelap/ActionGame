using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyManager : MonoBehaviour
{
    private bool hasKey = false;
    public Image uiIndicator;

    void Start()
    {
        uiIndicator.enabled = false;    
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Key"))
        {
            print("entered key");
            hasKey = true;
            uiIndicator.enabled = true;
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Door") && hasKey)
        {
            other.gameObject.GetComponent<EnemySpawner>().isSpawning = false;
            print("level complete");
        }
    }
}
