using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [Header("Health Settings")]
    public int currHealth;
    public int maxHealth;
    public HealthBar healthBar;

    void Start()
    {
        currHealth = maxHealth;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collided with object with tag: " + collision.gameObject.tag);
        if (collision.gameObject.CompareTag("Enemy"))
        {
            DamagePlayer(10);
        }
    }

    void DamagePlayer(int dmg)
    {
        currHealth -= dmg;
        healthBar.SetHealth(currHealth);
        if (currHealth <= 0) 
        { 
            KillPlayer();
        }
    }

    void KillPlayer()
    {
        SceneManager.LoadScene("game_over");
    }
}
