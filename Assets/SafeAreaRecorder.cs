using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeAreaRecorder : MonoBehaviour
{

 
     private Vector3 safeAreaDimensions = Vector3.zero;

private void Start()
    {
       
        Debug.Log("safe area script is going");
    }
public class SafeAreaInfo
{
    public Vector2 dimensions; // Store the dimensions of the safe area for the object.
    // You can add more information if needed.
}

// Create a dictionary to store safe area information for each object.
private Dictionary<GameObject, SafeAreaInfo> safeAreas = new Dictionary<GameObject, SafeAreaInfo>();


    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision with lava has been detected"); 
        GameObject collidedObject = collision.gameObject;

         // Get or create SafeAreaInfo for the collided object.
        SafeAreaInfo safeAreaInfo;
        if (!safeAreas.TryGetValue(collidedObject, out safeAreaInfo))
        {
        safeAreaInfo = new SafeAreaInfo();
        safeAreas[collidedObject] = safeAreaInfo;
         }

        // Get the dimensions of the collided object.
         Vector3 dimensions = collidedObject.GetComponent<Collider>().bounds.size;
        
        // Update the safe area dimensions based on the dimensions of the collided object.
        safeAreaDimensions.x = Mathf.Max(safeAreaDimensions.x, dimensions.x);
        safeAreaDimensions.z = Mathf.Max(safeAreaDimensions.z, dimensions.z);

    
    }

}
