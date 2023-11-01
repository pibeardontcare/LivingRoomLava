using UnityEngine;
using UnityEngine.UI;

public class ZPositionDisplay : MonoBehaviour
{
    public OVRCameraRig cameraRig;
    public Text zPositionText;

    

    void Update()
    {

         Vector3 playerPosition = cameraRig.trackingSpace.position;

        if (cameraRig != null && zPositionText != null)
        {
            float playerZPosition = cameraRig.trackingSpace.position.z;
            zPositionText.text = "Z-Position: " + playerZPosition.ToString("F2"); // Display z-position with 2 decimal places
            Debug.Log("Z-Position: " + playerZPosition.ToString("F2"));
        }
    }
}