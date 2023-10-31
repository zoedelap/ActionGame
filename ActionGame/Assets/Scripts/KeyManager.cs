using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KeyManager : MonoBehaviour
{
    private bool hasKey = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("key"))
        {
            print("entered key");
            hasKey = true;
            Destroy(other.gameObject);
        }

        if (other.CompareTag("exit") && hasKey)
        {
            print("level complete");
        }
    }
}
