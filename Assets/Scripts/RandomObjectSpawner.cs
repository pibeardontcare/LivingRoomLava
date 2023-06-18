using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomObjectSpawner : MonoBehaviour


{

public GameObject[] objectsToSpawn;
private int count = 0;
private float spawnHeight = .5f;
private float spawnRadius = .1f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("CreateObject", 0f, 5f);
    }

    // Update is called once per frame
    void CreateObject()
    {
        if(count >= 50)
        {
            CancelInvoke("CreateObject");
            return;
        }

        int randomIndex = Random.Range(0, objectsToSpawn.Length);

        Instantiate(objectsToSpawn[randomIndex], transform.position, transform.rotation);
        count++;
    }
}
