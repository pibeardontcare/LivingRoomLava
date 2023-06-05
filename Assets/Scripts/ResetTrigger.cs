using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetTrigger : MonoBehaviour
{
   public GameObject cubePrefab;
    public int minNumCubes = 10;
    public float xMin = -1.21f;
    public float xMax = 1.01f;
    public float yPos = 0.05f;
    public float zMin = 1.58f;
    public float zMax = 4.9f;
    public float maxDistance = 0.02f;

    private List<Vector3> generatedPositions = new List<Vector3>();

    private void OnTriggerEnter(Collider other)
    {
        // Only spawn cubes if the trigger has been entered
        if (other.gameObject.tag == "Player")
        {
            // Keep generating cubes until at least minNumCubes have been generated
            while (generatedPositions.Count < minNumCubes)
            {
                Vector3 randomPos = new Vector3(Random.Range(xMin, xMax), yPos, Random.Range(zMin, zMax));

                // Check if the randomly generated position is close enough to any existing cubes
                bool isTooClose = false;
                foreach (Vector3 existingPos in generatedPositions)
                {
                    if (Vector3.Distance(existingPos, randomPos) < maxDistance)
                    {
                        isTooClose = true;
                        break;
                    }
                }

                // If the randomly generated position is not too close to any existing cubes, instantiate a new cube at that position
                if (!isTooClose)
                {
                    GameObject newCube = Instantiate(cubePrefab, randomPos, Quaternion.identity);
                    generatedPositions.Add(randomPos);
                }
            }
        }
    }
}