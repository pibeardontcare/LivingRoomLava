using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerEventHandler : MonoBehaviour
{
    public Transform targetTransform;
    public float xMin = -1.15f;
    public float xMax = - .15f;
    public float zMin = .69f;
    public float zMax = 1.69f;
    public float yPos = 0;
    public GameObject prefabToSpawn;
    public int numberOfObjectsToSpawn = 10;
    

    private void Update()
    {
        float x = targetTransform.position.x;
        float z = targetTransform.position.z;
        if (x >= xMin && x <= xMax && z >= zMin && z <= zMax)
        {
            TriggerLavaRocks();
        }
    }

    void TriggerLavaRocks()
    {
        // Insert your action code here
        // Calculate the x and z intervals
            float xInterval = (xMax - xMin) / 9;
            float zInterval = (zMax - zMin) / 9;

            // Loop through and spawn 10 cubes
            for (int i = 0; i < 10; i++)
            {
                float xPos = Random.Range(xMin + i * xInterval, xMin + (i + 1) * xInterval);
                float zPos = zMin + i * zInterval;

                Vector3 cubePos = new Vector3(xPos, yPos, zPos);
                Instantiate(prefabToSpawn, cubePos, Quaternion.identity);
            }
    }
}


