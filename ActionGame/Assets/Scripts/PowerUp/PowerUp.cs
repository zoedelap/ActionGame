using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private int speedBoost = 5;
    [SerializeField] private int healthRegenBoost = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Pickup(other.gameObject);
        }
    }

    void Pickup(GameObject player)
    {
        Debug.Log("Power up picked up!");
        // increase health regen
        Health playerHealthScript = player.GetComponent<Health>();
        if (playerHealthScript != null) playerHealthScript.healthRegenerationPerSecond += healthRegenBoost;
        // increase player speed
        PlayerMovement playerMovemementScript = player.GetComponent<PlayerMovement>();
        if (playerMovemementScript != null) playerMovemementScript.speed += speedBoost;
        // destroy the power up
        Destroy(gameObject);
    }
}
