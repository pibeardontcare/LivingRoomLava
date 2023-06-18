using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectSpawner : MonoBehaviour


{

public GameObject objectToSpawn;
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

        Vector3 randomPos = Random.insideUnitSphere * spawnRadius;
        randomPos.y = spawnHeight;
        Instantiate(objectToSpawn, transform.position + randomPos, transform.rotation);
        count++;
    }
}
