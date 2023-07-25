using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class prefabSpawner : MonoBehaviour

{
    public GameObject[] prefabsToSpawn;
    public float spawnInterval = 2f;
    public float spawnHeight = 10f;
    public float spawnDistanceX = 5f;
    public float spawnDistanceZ = 5f;

    private float spawnTimer;

    private void Start()
    {
        spawnTimer = spawnInterval;
    }

    private void Update()
    {
        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0f)
        {
            SpawnRandomPrefab();
            spawnTimer = spawnInterval;
        }
    }

    private void SpawnRandomPrefab()
    {
        if (prefabsToSpawn.Length == 0)
        {
            Debug.LogWarning("No prefabs assigned to the spawner!");
            return;
        }

        int randomIndex = Random.Range(0, prefabsToSpawn.Length);
        GameObject prefab = prefabsToSpawn[randomIndex];

        float spawnPosX = Random.Range(-spawnDistanceX, spawnDistanceX);
        float spawnPosZ = Random.Range(-spawnDistanceZ, spawnDistanceZ);
        Vector3 spawnPosition = new Vector3(spawnPosX, spawnHeight, spawnPosZ);

        GameObject spawnedPrefab = Instantiate(prefab, spawnPosition, Quaternion.identity);
        spawnedPrefab.AddComponent<Rigidbody>();

        // Ensure the prefab has a collider so that physics interactions work properly
        if (!spawnedPrefab.GetComponent<Collider>())
        {
            spawnedPrefab.AddComponent<BoxCollider>();
        }
    }
}
