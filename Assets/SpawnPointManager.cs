using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointManager : MonoBehaviour
{

    public Transform spawnPoint; // Reference to your spawn point Transform
    public Camera oculusCamera; // Reference to your Oculus main camera

    void Start()
    {
        if (oculusCamera == null)
        {
            Debug.LogError("Oculus main camera not assigned. Camera may not spawn correctly.");
            return;
        }

        if (spawnPoint != null)
        {
            // Move the Oculus camera to the spawn point's position and rotation
            oculusCamera.transform.position = spawnPoint.position;
            oculusCamera.transform.rotation = spawnPoint.rotation;
        }
        else
        {
            Debug.LogError("Spawn point not assigned. Camera may not spawn correctly.");
        }
    }
}



