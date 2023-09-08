using UnityEngine;

public class ShowHideObject : MonoBehaviour
{public GameObject objectToShowHide;
    public ParticleSystem particleEmitter;
    public Camera oculusMainCamera;

    // Define x and z boundaries for the object
    public float minX;
    public float maxX;
    public float minZ;
    public float maxZ;

    private bool isInsidePerimeter = false;

    private void Start()
    {
        // Hide the objectToShowHide and stop the particle emitter at the start
        objectToShowHide.SetActive(false);
        particleEmitter.Stop();
    }

    bool IsCameraWithinObjectPerimeter()
    {
        // Get the camera's position in world coordinates (ignoring y-axis).
        Vector3 cameraPosition = oculusMainCamera.transform.position;
        cameraPosition.y = 0f; // Ignore the y-axis.

        // Check if the camera's x and z positions are within the specified boundaries.
        bool isWithinBounds = (cameraPosition.x >= minX && cameraPosition.x <= maxX &&
                               cameraPosition.z >= minZ && cameraPosition.z <= maxZ);

        return isWithinBounds;
    }

    void Update()
    {
        bool isWithinPerimeter = IsCameraWithinObjectPerimeter();

        // Check if the camera has entered or exited the perimeter
        if (isWithinPerimeter != isInsidePerimeter)
        {
            isInsidePerimeter = isWithinPerimeter;

            // Do something based on whether the camera is within the object's perimeter.
            if (isInsidePerimeter)
            {
                Debug.Log("Camera is within the object's perimeter.");
                // Add your code here for when the camera is within the object.

                objectToShowHide.SetActive(true);
                particleEmitter.Play();
            }
            else
            {
                Debug.Log("Camera is NOT within the object's perimeter.");
                // Add your code here for when the camera is NOT within the object.

                objectToShowHide.SetActive(false);
                particleEmitter.Stop();
            }
        }
    }
}
