using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireworkSpawner : MonoBehaviour
{
 public GameObject[] prefabs;
    public float minSpawnDelay = 0.05f;
    public float maxSpawnDelay = 0.09f;
    public float spawnDuration = 2f;
    public float spawnRadiusX = 4f;
    public float spawnRadiusZ = 2f;
    public float spawnRadiusY = 1f;

    public AudioSource audioSource; // The audio source to play the sound from

    private bool audioPlayed = false;
    private float elapsedTime = 0f;

    public void TriggerMethod()
    {
        StartCoroutine(SpawnRandomPrefab());

        if (!audioPlayed && audioSource != null)
        {
            audioSource.PlayOneShot(audioSource.clip);
            audioPlayed = true;
        }
    }

    private IEnumerator SpawnRandomPrefab()
    {
        while (elapsedTime < spawnDuration)
        {
            elapsedTime += Time.deltaTime;

            // Choose a random prefab
            int prefabIndex = Random.Range(0, prefabs.Length);

            // Choose a random position within the spawn radius
            Vector3 spawnPosition = transform.position + new Vector3(
                Random.Range(-spawnRadiusX, spawnRadiusX),
                Random.Range(-spawnRadiusY, spawnRadiusY),
                Random.Range(-spawnRadiusZ, spawnRadiusZ)
            );

            // Spawn the prefab at the random position
            Instantiate(prefabs[prefabIndex], spawnPosition, Quaternion.identity);

            // Wait for a random amount of time before spawning the next prefab
            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
        }
    }
}

