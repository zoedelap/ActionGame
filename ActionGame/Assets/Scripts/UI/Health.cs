using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [Header("Health Settings")]
    private int _currHealth;
    public int currHealth { 
        get { return _currHealth; } 
        set { 
            _currHealth = value; 
            healthBar.SetHealth(_currHealth);
            if (_currHealth < lowHealthThreshold) EnableAPowerUp();
        } 
    }
    public int maxHealth = 100;
    public HealthBar healthBar;

    [SerializeField] private int maxHealthRegenerationPerSecond = 25;
    private int _healthRegenerationPerSecond = 0;
    public int healthRegenerationPerSecond { get { return _healthRegenerationPerSecond; } set { _healthRegenerationPerSecond = Mathf.Min(value, maxHealthRegenerationPerSecond); } }
    public GameObject gameOverPanel; // in future this should be moved to a designated game state manager and include the nextLevelPanel object seen in KeyManager.cs

    [Header("Power Up Assistance Settings")]
    [SerializeField] private GameObject[] powerUpsToEnableOnLowHealth;
    [SerializeField] private int lowHealthThreshold = 25;
    [SerializeField] private float powerUpSpawningCooldown = 5.0f;
    private Queue<GameObject> powerUpQueue = new Queue<GameObject>();
    private bool powerUpSpawningIsEnabled = true;
    

    void Start()
    {
        currHealth = maxHealth;
        InvokeRepeating(nameof(RegenerateHealth), 0, 1.0f);
        foreach (GameObject powerUp in powerUpsToEnableOnLowHealth)
        {
            powerUpQueue.Enqueue(powerUp);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
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
        // SceneManager.LoadScene("game_over");
        Time.timeScale = 0;
        gameOverPanel.SetActive(true);
    }

    private void RegenerateHealth()
    {
        currHealth = Mathf.Min(maxHealth, currHealth + _healthRegenerationPerSecond);
        healthBar.SetHealth(currHealth);
    }

    private void EnableAPowerUp()
    {
        Debug.Log("low health, attempting to enable a power up");
        if (powerUpSpawningIsEnabled && powerUpQueue.Count > 0)
        {
            powerUpQueue.Dequeue().SetActive(true);
            powerUpSpawningIsEnabled = false;
            Invoke(nameof(EndPowerUpCooldown), powerUpSpawningCooldown);
        }
    }

    private void EndPowerUpCooldown() { powerUpSpawningIsEnabled = true; }
}
