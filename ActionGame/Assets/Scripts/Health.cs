using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [Header("Health Settings")]
    private int _currHealth;
    public int currHealth { get { return _currHealth; } set { _currHealth = value; healthBar.SetHealth(_currHealth); } }
    public int maxHealth;
    public HealthBar healthBar;

    [SerializeField] private int maxHealthRegenerationPerSecond = 25;
    private int _healthRegenerationPerSecond = 0;
    public int healthRegenerationPerSecond { get { return _healthRegenerationPerSecond;} set { _healthRegenerationPerSecond = Mathf.Min(value, maxHealthRegenerationPerSecond); } }

    void Start()
    {
        currHealth = maxHealth;
        InvokeRepeating(nameof(RegenerateHealth), 0, 1.0f);
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
        if (currHealth <= 0) 
        { 
            KillPlayer();
        }
    }

    void KillPlayer()
    {
        SceneManager.LoadScene("game_over");
    }

    private void RegenerateHealth()
    {
        currHealth = Mathf.Min(maxHealth, currHealth + _healthRegenerationPerSecond);
        healthBar.SetHealth(currHealth);
    }
}
