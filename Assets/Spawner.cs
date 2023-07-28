using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefabToSpawn;
    public GameObject floor;

    // Define the boundaries for spawning
    private float minX;
    private float maxX;
    private float minZ;
    private float maxZ;

    // Height at which the prefabs will spawn
    private float spawnHeight = 3f;

    // Set the time interval for spawning (in seconds)
    public float spawnInterval = 1f;

    private void Start()
    {
        // Calculate the boundaries for spawning based on the floor object's position and dimensions
        Vector3 floorPosition = floor.transform.position;
        Vector3 floorSize = floor.GetComponent<Renderer>().bounds.size;

        minX = floorPosition.x - floorSize.x / 2f;
        maxX = floorPosition.x + floorSize.x / 2f;
        minZ = floorPosition.z - floorSize.z / 2f;
        maxZ = floorPosition.z + floorSize.z / 2f;

        // Start spawning prefabs at the specified interval
        StartCoroutine(SpawnPrefabs());
    }

    private IEnumerator SpawnPrefabs()
    {
        while (true)
        {
            // Generate a random position within the defined boundaries
            float randomX = Random.Range(minX, maxX);
            float randomZ = Random.Range(minZ, maxZ);

            // Instantiate the prefab at the random position and spawnHeight
            Vector3 spawnPosition = new Vector3(randomX, spawnHeight, randomZ);
            Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
