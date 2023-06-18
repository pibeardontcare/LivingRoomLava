using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaSpawner : MonoBehaviour
{
    public GameObject[] prefabs; // array of 3 prefabs
    public float[] xValues = { -0.6f, -0.2f, 0.2f, 0.6f, 1f }; // array of x values for grid
    public float[] zValues = { 1.3f, 1.6f, 2f, 2.4f, 2.7f, 3.2f, 3.6f, 4f }; // array of z values for grid
    public int numPrefabsToSpawn = 15; // number of prefabs to spawn
    public float minDistanceBetweenPrefabs = 1f; // minimum distance between prefabs
    public LayerMask spawnableLayer; // layer mask for spawnable areas

    public void TriggerMethod()
    {
        // shuffle prefabs array
        for (int i = 0; i < prefabs.Length; i++)
        {
            GameObject temp = prefabs[i];
            int randomIndex = Random.Range(i, prefabs.Length);
            prefabs[i] = prefabs[randomIndex];
            prefabs[randomIndex] = temp;
        }

        // spawn prefabs at random positions
        int numSpawned = 0;
        while (numSpawned < numPrefabsToSpawn)
        {
            // pick a random x and z value
            float x = xValues[Random.Range(0, xValues.Length)];
            float z = zValues[Random.Range(0, zValues.Length)];

            // check if position is valid
            Vector3 spawnPosition = new Vector3(x, 0.23f, z);
            bool isValidPosition = true;

            Collider[] hitColliders = Physics.OverlapSphere(spawnPosition, minDistanceBetweenPrefabs, spawnableLayer);
            foreach (Collider hitCollider in hitColliders)
            {
                isValidPosition = false;
                break;
            }

            if (isValidPosition)
            {
                // instantiate random prefab
                GameObject prefab = prefabs[numSpawned % prefabs.Length];
                Instantiate(prefab, spawnPosition, Quaternion.identity);
                Debug.Log("Spawned prefab: " + prefab.name + " at position: " + spawnPosition);

                numSpawned++;
            }
        }
    }
}
