using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [Header("Health Settings")]
    public int health;
    public int maxHealth;

    void Awake()
    {
        
    }
    void Update()
    {
        if (health == 0)
        {
            Debug.Log("HM: player died");
        }
    }

    void OnCollisionEnter(Collision coll)
    {
        switch (coll.gameObject.name)
        {
            case "Mouse":
                Debug.Log("HM: Hit a mouse");
                break;
            case "Bird":
                Debug.Log("HM: Hit a bird");
                break;
            default:
                Debug.Log("HM: Hit something unknown");
                break;

        }
    }
}
