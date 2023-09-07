using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class SafeAreaRecorder : MonoBehaviour
{
    public Camera oculusMainCamera;
    public Text safeAreaText; //print safe area for debug

    public  bool isInsideAnyObject = false;
    // Create a dictionary to store safe area information for each object.
    private Dictionary<GameObject, SafeAreaInfo> safeAreas = new Dictionary<GameObject, SafeAreaInfo>();

    private void Start()
    {
        Debug.Log("Safe area script is running.");
    }

   private void Update()
{
    if (oculusMainCamera == null)
    {
        Debug.LogWarning("Oculus Main Camera is not assigned.");
        return;
    }

    // Get the position of the Oculus Main Camera.
    Vector3 cameraPosition = oculusMainCamera.transform.position;

 

    foreach (var kvp in safeAreas)
    {
        SafeAreaInfo safeAreaInfo = kvp.Value;

        // Calculate the boundaries of the safe area.
        Vector2 safeAreaMin = safeAreaInfo.Position - safeAreaInfo.Dimensions / 2;
        Vector2 safeAreaMax = safeAreaInfo.Position + safeAreaInfo.Dimensions / 2;

        // Check if the camera's position is within the safe area boundaries.
        if (cameraPosition.x >= safeAreaMin.x && cameraPosition.x <= safeAreaMax.x &&
            cameraPosition.z >= safeAreaMin.y && cameraPosition.z <= safeAreaMax.y)
        {
            // The camera is inside at least one object's safe area.
            isInsideAnyObject = true;
            break; // No need to continue checking other objects.
        }
    }

    if (isInsideAnyObject)
    {
        //Debug.Log("Camera is inside the safe area of at least one object.");
    }
    else
    {
        //Debug.Log("Camera is outside the safe area of all objects.");
    }
}

    public class SafeAreaInfo
    {
        public Vector2 Dimensions { get; set; } // Store the dimensions of the safe area for the object.
        public Vector2 Position { get; set; }   // Store the position of the safe area for the object.

        // You can add more information if needed.
    }

    private void OnCollisionEnter(Collision collision)
    {
       
        GameObject collidedObject = collision.gameObject;
         Debug.Log("Collision with: " + collidedObject);
        // Get or create SafeAreaInfo for the collided object.
        SafeAreaInfo safeAreaInfo;
        if (!safeAreas.TryGetValue(collidedObject, out safeAreaInfo))
        {
            safeAreaInfo = new SafeAreaInfo();
            safeAreas[collidedObject] = safeAreaInfo;
        }

        // Get the dimensions of the collided object.
        Vector3 dimensions = collidedObject.GetComponent<Collider>().bounds.size;

        // Get the position of the collided object.
        Vector3 position = collidedObject.transform.position;

         // Check if the object's y-axis (up vector) is pointing upwards.
        bool isUpward = Vector3.Dot(collidedObject.transform.up, Vector3.up) > 0.9f; // Adjust the threshold as needed.

        // Update the safe area dimensions based on the dimensions of the collided object.
        safeAreaInfo.Dimensions = new Vector2(dimensions.x, dimensions.z);

        // Update the safe area position based on the position of the collided object.
        safeAreaInfo.Position = new Vector2(position.x, position.z);

        Debug.Log("Safe area dimensions: " + safeAreaInfo.Dimensions);
        Debug.Log("Safe area position: " + safeAreaInfo.Position);

        if (isUpward)
        {
        Debug.Log("The object landed with its y-axis pointing up.");
        }
        
        else
        {
        Debug.Log("The object did not land with its y-axis pointing up (landed on its edge).");

    // Calculate the rotation needed to align the object with the desired landing orientation.
        Quaternion targetRotation = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f); // Rotate randomly on the y-axis.

        // Set the object's rotation to the target rotation.
        collidedObject.transform.rotation = targetRotation;
        // // Check if the object has a Rigidbody component.
        // Rigidbody rb = collidedObject.GetComponent<Rigidbody>();
        //     if (rb == null)
        //     {
        //     // If the object doesn't have a Rigidbody, add one dynamically.
        //     rb = collidedObject.AddComponent<Rigidbody>();
        //     }

        // // Calculate the direction to apply the force (perpendicular to the up vector).
        // Vector3 forceDirection = Vector3.Cross(collidedObject.transform.up, Vector3.up).normalized;

        // // Apply a small force to knock the object off its edge.
        // float forceMagnitude = 100f; // Adjust the force magnitude as needed.
        // rb.AddForce(forceDirection * forceMagnitude, ForceMode.Impulse);
        
        // Update the safe area info with the new position and dimensions.
        safeAreaInfo.Dimensions = new Vector2(dimensions.x, dimensions.z);
        safeAreaInfo.Position = new Vector2(position.x, position.z);}

        // Update the UI Text element with the safe area data
        UpdateSafeAreaUIText(safeAreaInfo);
        
    }


    // Function to update the UI Text element with safe area data
    private void UpdateSafeAreaUIText(SafeAreaInfo safeAreaInfo)
    {
        if (safeAreaText != null)
        {
            // Format the safe area information as a string
            string infoText = "Safe Area Dimensions: " + safeAreaInfo.Dimensions.ToString() +
                              "\nSafe Area Position: " + safeAreaInfo.Position.ToString();

            // Set the UI Text element's text to the formatted information
            safeAreaText.text = infoText;
        }
    }
}
