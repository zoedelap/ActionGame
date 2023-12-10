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

    [SerializeField] private int maxHealthRegenerationPerSecond = 25;
    private int _healthRegenerationPerSecond = 0;
    public int healthRegenerationPerSecond { get { return _healthRegenerationPerSecond; } set { _healthRegenerationPerSecond = Mathf.Min(value, maxHealthRegenerationPerSecond); } }
    public GameObject gameOverPanel; // in future this should be moved to a designated game state manager and include the nextLevelPanel object seen in KeyManager.cs

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
        healthBar.SetHealth(currHealth);
        if (currHealth <= 0)
        {
            KillPlayer();
        }
    }

    void KillPlayer()
    {
        // SceneManager.LoadScene("game_over");
        Time.timeScale = 0;
        gameOverPanel.SetActive(true);
    }

    private void RegenerateHealth()
    {
        currHealth = Mathf.Min(maxHealth, currHealth + _healthRegenerationPerSecond);
        healthBar.SetHealth(currHealth);
    }
}
