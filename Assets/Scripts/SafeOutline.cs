using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeOutline : MonoBehaviour
{

    public Material insideMaterial; // Material to use when the VR headset is inside the prefab bounds
    public Material outsideMaterial; // Material to use when the VR headset is outside the prefab bounds
    public Material neutralMaterial; 
    public GameObject prefabToSpawn; // Assign the prefab to be spawned in the Inspector
    [SerializeField] private Renderer SignFaceRenderer;
    [SerializeField] private GameObject signFace;
    
    private bool hasCollided = false;
    private bool hasSpawnedPrefab = false; // Flag to track if the prefab has been spawned
    private Collider floorCollider; 
    private GameObject vrHeadsetReference; // Reference to the VR headset GameObject
    public GameObject safeArea; // Reference to the start Safe Area


   private void Start()
{
    // Access the GameManager and retrieve the VR headset reference
    
    

    // Get the Renderer component attached to the target object
    if (signFace != null)
    {
        SignFaceRenderer = signFace.GetComponent<Renderer>();
    }
    else
    {
        Debug.LogError("signFace GameObject is not assigned in the Inspector.");
    }




    // Check VR headset (camera) position against the corner vertices at the start
    bool isInsideSafeArea = IsVRHeadsetInsideSafeArea();

    // Set the material to the neutral color material if inside the safe area
    if (isInsideSafeArea)
    {
        Debug.Log("VR headset is inside the safe area at the start.");
        SignFaceRenderer.material = neutralMaterial;
    }

    Debug.Log("SafeOutline script has started.");
}

    private void OnTriggerEnter(Collider other)
    {
        if (other == floorCollider)
        {
            hasCollided = true;
               
        }
        else
        {
            Debug.Log("There are other colliders you know");
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
        bool isInsidePrefabBounds = IsVRHeadsetInsidePrefabBounds(corners);
        bool isInsideSafeArea = IsVRHeadsetInsideSafeArea();

        // Check VR headset (camera) position against the corner vertices
        if (isInsidePrefabBounds || isInsideSafeArea)
        {
            if (isInsidePrefabBounds)
            {
                // VR headset (camera) is inside the prefab bounds
                Debug.Log("VR headset is inside the prefab bounds.");
                // Set the material to the insideMaterial
                SignFaceRenderer.material = outsideMaterial;
            }
            else
            {
                // VR headset is inside the safe area but not inside the prefab bounds
                Debug.Log("VR headset is inside the safe area but not inside the prefab bounds.");
                // Set the material to the neutral color material
                SignFaceRenderer.material = neutralMaterial;
            }
        }
        else
        {
            // VR headset (camera) is outside the prefab bounds
            Debug.Log("VR headset is outside the prefab bounds.");
            // Set the material to the outsideMaterial
            SignFaceRenderer.material = insideMaterial;
        }
    }

    Debug.Log("Update method is being called.");
    // Debug.Log("Current Material: " + SignFaceRenderer.material.name);
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

         // Get the current position of the collision point
        Vector3 currentPosition = transform.position;

         // Adjust the y-component of the position to be 0.15 lower
        currentPosition.y -= 0.15f;
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

    private bool IsVRHeadsetInsideSafeArea()
    {
        // Get the VR headset (camera) position from the public variable
        Vector3 headsetPosition = vrHeadsetReference.transform.position;

        // Get the position and scale of the safe area cube
        Vector3 cubePosition = safeArea.transform.position;
        Vector3 cubeScale = safeArea.transform.localScale;
        float halfCubeX = cubeScale.x * 0.5f;
        float halfCubeZ = cubeScale.z * 0.5f;

        // Check if the VR headset (camera) position is within the safe area cube bounds
        if (headsetPosition.x >= cubePosition.x - halfCubeX && headsetPosition.x <= cubePosition.x + halfCubeX &&
            headsetPosition.z >= cubePosition.z - halfCubeZ && headsetPosition.z <= cubePosition.z + halfCubeZ)
        {
            return true; // VR headset (camera) is inside the bounds of the safe area cube
        }

        return false; // VR headset (camera) is outside the bounds of the safe area cube
    }
}
