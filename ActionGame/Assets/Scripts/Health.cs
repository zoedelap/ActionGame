using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    void Update(){
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DamagePlayer(10);
        }
    }
    void DamagePlayer(int dmg)
    {
        currHealth -= dmg;
        healthBar.SetHealth(currHealth);
    }
    
}
