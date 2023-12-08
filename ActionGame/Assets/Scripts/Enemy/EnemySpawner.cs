using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Inscribed")]
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform enemySpawnPoint;
    [SerializeField] private float spawnDecreaseRate = 0.95f; 

    [Header("Dynamic")]
    private bool _isSpawning = true;
    public float spawnRate = 5.0f;

    public Transform enemyTarget;

    private Renderer[] doorRenderers; 

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
            _isSpawning = value;
            UpdateMaterial();
        }
    }

    private void Start()
    {
        // start spawning if set to spawn by default
        if (_isSpawning) Invoke(nameof(SpawnEnemy), Random.Range(0, 2.0f));
        doorRenderers = GetComponentsInChildren<Renderer>();
        UpdateMaterial();
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

        // reduce the amount of time before the next spawn, down to 1 second between spawns
        spawnRate = Mathf.Max(1.0f, spawnRate * spawnDecreaseRate);
        Debug.Log("spawn rate: " + spawnRate);

        // wait the appropriate delay before spawning again
        Invoke(nameof(SpawnEnemy), spawnRate);
    }

    private void UpdateMaterial()
    {
        Color color = new Color(0.4056604f, 0.2324893f, 0.05931826f, 1.0f);
        if (_isSpawning) color = Color.black;
        foreach (Renderer renderer in doorRenderers)
        {
            renderer.material.SetColor("_Color", color);
        }

    }
}
