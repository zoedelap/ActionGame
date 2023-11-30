using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Inscribed")]
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform enemySpawnPoint;

    [Header("Dynamic")]
    private bool _isSpawning = true;
    public float spawnRate = 1.0f;

    public Transform enemyTarget;

    public bool isSpawning
    {
        get {  return ( _isSpawning ); }
        set { 
            // going from not spawning to spawning
            if (!_isSpawning && value)
            {
                // restart spawning
                SpawnEnemy();
            }
        }
    }

    private void Start()
    {
        // start spawning if set to spawn by default
        if (_isSpawning) SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        // if we have stopped spawning since the last call, stop
        if (!_isSpawning) return;

        // spawn
        GameObject enemyGO = Instantiate(enemyPrefab);
        // move to spawn point
        enemyGO.transform.position = enemySpawnPoint.position;
        // set target
        EnemyNavigation enemyNavScript = enemyGO.GetComponentInChildren<EnemyNavigation>();
        if (enemyNavScript != null)
        {
            enemyNavScript.target = enemyTarget;
            Debug.Log("EnemyNavigationScript target set to " + enemyNavScript.target.name);
        }
        else
        {
            Debug.LogWarning("EnemyNavigationScript not found on prefab!");
        }

        // wait the appropriate delay before spawning again
        Invoke(nameof(SpawnEnemy), spawnRate);
    }

}
