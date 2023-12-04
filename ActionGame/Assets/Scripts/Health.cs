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
        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            DamagePlayer(10);
        }*/
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
    }
}
