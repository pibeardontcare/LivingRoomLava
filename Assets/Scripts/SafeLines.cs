using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject prefabToSpawn;
    public int numberOfObjectsToSpawn;
    public float spawnRadius;
    public Material lineMaterial;

    private GameObject[] spawnedObjects;
    private LineRenderer lineRenderer;

    void Start()
    {
        SpawnObjects();
        SetupLineRenderer();
    }

    void SpawnObjects()
    {
        spawnedObjects = new GameObject[numberOfObjectsToSpawn];

        for (int i = 0; i < numberOfObjectsToSpawn; i++)
        {
            Vector3 randomPosition = Random.insideUnitCircle * spawnRadius;
            Vector3 spawnPosition = new Vector3(randomPosition.x, 0f, randomPosition.y) + transform.position;
            spawnedObjects[i] = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity, transform);
        }
    }

    void SetupLineRenderer()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = lineMaterial;
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.positionCount = numberOfObjectsToSpawn + 1; // +1 to close the loop

        for (int i = 0; i < numberOfObjectsToSpawn; i++)
        {
            lineRenderer.SetPosition(i, spawnedObjects[i].transform.position);
        }

        // Close the loop by connecting the last point with the first point
        lineRenderer.SetPosition(numberOfObjectsToSpawn, spawnedObjects[0].transform.position);
    }
}
