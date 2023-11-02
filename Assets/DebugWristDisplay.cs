using UnityEngine;
using UnityEngine.UI;

public class ZPositionDisplay : MonoBehaviour
{
    public GameObject trackingObject; // Assign the game object you want to track
    public Text zPositionText; // Assign the UI Text component

    void Update()
    {
        if (trackingObject != null && zPositionText != null)
        {
            Vector3 objectPosition = trackingObject.transform.position;
            zPositionText.text = "Position: " + objectPosition.ToString("F2");
            Debug.Log("Position: " + objectPosition.ToString("F2"));
        }
    }
}