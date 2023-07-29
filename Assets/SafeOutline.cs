using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeOutline : MonoBehaviour
{

    public Material insideMaterial; // Material to use when the VR headset is inside the prefab bounds
    public Material outsideMaterial; // Material to use when the VR headset is outside the prefab bounds
    public GameObject prefabToSpawn; // Assign the prefab to be spawned in the Inspector
    private bool hasCollided = false;
    private bool hasSpawnedPrefab = false; // Flag to track if the prefab has been spawned
    private Collider floorCollider; 
    private GameObject vrHeadsetReference; // Reference to the VR headset GameObject

    private Renderer SignObjectRenderer; // Reference to the Renderer component of the object to change color

    

    private void Start()
    {
        // Access the GameManager and retrieve the VR headset reference
        vrHeadsetReference = GameManager.instance.vrHeadsetReference;
        floorCollider = GameManager.instance.floorCollider;

         // Get the Renderer component attached to the target object
      
        SignObjectRenderer = GameManager.instance.signFace.GetComponent<Renderer>();
    
    }

 private void OnTriggerEnter(Collider other)
{
   if (other == floorCollider)
    {
        hasCollided = true;
    }
    else
    {
        Debug.Log("No collisions are happening");
    }
}

    private void Update()
    {
        if (hasCollided && !hasSpawnedPrefab) // Check if collided and not spawned prefab yet
        {
            // Calculate and print the corner vertices
            Vector3[] corners = CalculateVertices();
            
            // Spawn a new prefab at the same position
            
            SpawnNewPrefabAtCollisionPoint();
            hasSpawnedPrefab = true;

            

            // Check VR headset (camera) position against the corner vertices
            if (IsVRHeadsetInsidePrefabBounds(corners))
            {
                // VR headset (camera) is inside the prefab bounds
                Debug.Log("VR headset is inside the prefab bounds.");

                
                // Set the material to the insideMaterial
                SignObjectRenderer.material = insideMaterial;
            }
            else
            {
                // VR headset (camera) is outside the prefab bounds
                Debug.Log("VR headset is outside the prefab bounds.");
                // Set the material to the outsideMaterial
                SignObjectRenderer.material = outsideMaterial;
            }
        }
    }

    private Vector3[] CalculateVertices()
    {
        // Get the position and scale of the prefab
        Vector3 position = transform.position;
        Vector3 scale = transform.localScale;

        // Calculate the corner vertices
        Vector3[] corners = new Vector3[8];
        corners[0] = position + new Vector3(-0.5f * scale.x, -0.5f * scale.y, -0.5f * scale.z);
        corners[1] = position + new Vector3(0.5f * scale.x, -0.5f * scale.y, -0.5f * scale.z);
        corners[2] = position + new Vector3(-0.5f * scale.x, -0.5f * scale.y, 0.5f * scale.z);
        corners[3] = position + new Vector3(0.5f * scale.x, -0.5f * scale.y, 0.5f * scale.z);
        corners[4] = position + new Vector3(-0.5f * scale.x, 0.5f * scale.y, -0.5f * scale.z);
        corners[5] = position + new Vector3(0.5f * scale.x, 0.5f * scale.y, -0.5f * scale.z);
        corners[6] = position + new Vector3(-0.5f * scale.x, 0.5f * scale.y, 0.5f * scale.z);
        corners[7] = position + new Vector3(0.5f * scale.x, 0.5f * scale.y, 0.5f * scale.z);

        // Print the corner vertices to the console
        for (int i = 0; i < 8; i++)
        {
            Debug.Log("Corner " + i + ": " + corners[i]);
        }

        return corners; // Return the corner vertices array
    }

       private void SpawnNewPrefabAtCollisionPoint()
    {
        // Instantiate a new prefab at the same position
        Instantiate(prefabToSpawn, transform.position, Quaternion.identity);
    }

    private bool IsVRHeadsetInsidePrefabBounds(Vector3[] corners)
    {
        if (vrHeadsetReference == null)
        {
            Debug.LogError("VR headset reference is not assigned in the Inspector.");
            return false;
        }

        // Get the VR headset (camera) position from the public variable
        Vector3 headsetPosition = vrHeadsetReference.transform.position;

        // Check if the VR headset (camera) position is within the prefab bounds
        for (int i = 0; i < corners.Length; i++)
        {
            if (headsetPosition.x < corners[i].x || headsetPosition.x > corners[i].x + transform.localScale.x ||
                headsetPosition.y < corners[i].y || headsetPosition.y > corners[i].y + transform.localScale.y ||
                headsetPosition.z < corners[i].z || headsetPosition.z > corners[i].z + transform.localScale.z)
            {
                return false; // VR headset (camera) is outside the bounds of at least one corner
            }
        }

        return true; // VR headset (camera) is inside the bounds of all corners
    }
}
