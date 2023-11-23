using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision coll){
        if(coll.gameObject.tag == "Enemy"){
            Debug.Log("PM: Enemy Collision");
            health -= 10;
            if(health <= 0){
                Destroy(gameObject);
            }
        }
    }
}
