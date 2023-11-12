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
    [SerializeField] private float minDist = 1;
    [SerializeField] private float jumpAmount = 5;

    private void Start()
    {
        enemy = transform.parent.gameObject;
        enemyRB = enemy.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Vector3 direction = (target.transform.position - enemy.transform.position).normalized;
        // enemy.transform.LookAt(target);

        if (Vector3.Distance(enemy.transform.position, target.position) >= minDist)
        {
            Debug.Log("distance: " + Vector3.Distance(enemy.transform.position, target.position) + ", minDistance: " + minDist);
            enemy.transform.position += speed * Time.deltaTime * new Vector3(direction.x, 0, 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        Debug.Log("jumping (trigger)");
        enemyRB.AddForce(Vector3.up * jumpAmount, ForceMode.Impulse);
    }
}
