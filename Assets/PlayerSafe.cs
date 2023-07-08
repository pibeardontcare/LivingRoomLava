using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSafe : MonoBehaviour

{
    public Transform playerTransform;
    public GameObject[] cubes;

    private void Update()
    {
        bool playerInsideCube = false;

        foreach (GameObject cube in cubes)
        {
            Vector3 cubeCenter = cube.transform.position;
            float distance = Vector3.Distance(playerTransform.position, cubeCenter);

            if (distance <= 0.5f)
            {
                // Player is within the boundary of this cube
                playerInsideCube = true;
                break;
            }
        }

        if (playerInsideCube)
        {
            // Player is within the boundary of at least one cube
            // Allow them to proceed or perform desired actions
            Debug.Log("Player is inside a cube boundary.");
        }
        else
        {
            // Player is outside the boundary of all cubes
            // Restrict their movement or take appropriate action
            Debug.Log("Player is outside all cube boundaries.");
        }
    }
}


