using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillowSwap : MonoBehaviour
{
    public LayerMask prefabLayer;
    public Collider interactionCollider; // Collider to interact with
    public Material brightGreenMaterial;
    public float newPrefabScale = 1.1f;
    public GameObject prefabToSpawn; // Assign the prefab you want to spawn in the Inspector

    private List<Vector3> prefabPositions = new List<Vector3>(); // List to store positions of spawned prefabs

    private void OnTriggerEnter(Collider other)
    {
        if (other == interactionCollider && IsPrefabObject(other.gameObject))
        {
            DestroyAndSpawnPrefab(other.gameObject);
        }
    }

    private bool IsPrefabObject(GameObject obj)
    {
        // Check if the object belongs to the prefabLayer
        return (1 << obj.layer & prefabLayer.value) != 0;
    }

    private void DestroyAndSpawnPrefab(GameObject prefabToDestroy)
    {
        // Store the position of the prefab before destroying it
        Vector3 spawnPosition = prefabToDestroy.transform.position;

        // Add the position to the list of prefab positions
        prefabPositions.Add(spawnPosition);

        // Destroy the old prefab
        Destroy(prefabToDestroy);

        // Instantiate a new prefab at the same position
        GameObject newPrefab = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);

        // Change materials to bright green for all meshes in the new prefab
        MeshRenderer[] meshRenderers = newPrefab.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer renderer in meshRenderers)
        {
            Material[] materials = renderer.materials;
            for (int i = 0; i < materials.Length; i++)
            {
                materials[i] = brightGreenMaterial;
            }
            renderer.materials = materials;
        }

        // Scale the new prefab
        newPrefab.transform.localScale *= newPrefabScale;
    }

    public List<Vector3> GetPrefabPositions()
    {
        // Return the list of prefab positions
        return prefabPositions;
    }
}
