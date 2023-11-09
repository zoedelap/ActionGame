using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavigation : MonoBehaviour
{
    private GameObject enemy = null;
    private Rigidbody enemyRB = null;

    public Transform target = null;
    
    [SerializeField] private float speed = 5;
    [SerializeField] private float minDist = 0.25f;
    [SerializeField] private float jumpAmount = 5;

    private void Start()
    {
        enemy = transform.parent.gameObject;
        enemyRB = enemy.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        enemy.transform.LookAt(target);

        if (Vector3.Distance(enemy.transform.position, target.position) >= minDist)
        {
            enemy.transform.position += enemy.transform.forward * speed * Time.deltaTime;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("jumping");
        enemyRB.AddForce(Vector3.up * jumpAmount, ForceMode.Impulse);
    }
}
