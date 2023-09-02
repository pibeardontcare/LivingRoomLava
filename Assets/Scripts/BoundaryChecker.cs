using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoundaryChecker : MonoBehaviour
{
    public delegate void BoundaryObjectCollidedEventHandler(GameObject boundaryObject);
    public event BoundaryObjectCollidedEventHandler OnBoundaryObjectCollided;
    public Transform vrCameraTransform; // Reference to the VR camera transform
    public List<GameObject> boundaryObjects; // List of game objects representing the boundaries
    public Text boundariesText; // Reference to the UI 
    private bool isInsideBoundaries = false; // Flag to track if the user is inside the boundaries

    public delegate void BoundaryEnterEventHandler();
    public event BoundaryEnterEventHandler OnEnterBoundaries;

    public delegate void BoundaryExitEventHandler();
    public event BoundaryExitEventHandler OnExitBoundaries;

    public AudioSource collisionAudioSource; // Reference to the AudioSource component

    public AudioClip soundForZAndXAxisUp;
    public AudioClip soundForYAxisUp;

private void Start()
{
    // Assign audio clips programmatically (assuming they're in the Resources folder)
    collisionAudioSource = GetComponent<AudioSource>();
    soundForZAndXAxisUp = Resources.Load<AudioClip>("SoundForZAndXAxisUp");
    soundForYAxisUp = Resources.Load<AudioClip>("SoundForYAxisUp");
}

    // Update is called once per frame
    private void Update()
    {
        // Check if the user is inside the boundaries
        bool isInside = CheckInsideBoundaries();

        // Check for boundary enter/exit events and trigger them accordingly
        if (isInside && !isInsideBoundaries)
        {
            isInsideBoundaries = true;
            if (OnEnterBoundaries != null)
            {
                OnEnterBoundaries();
            }
        }
        else if (!isInside && isInsideBoundaries)
        {
            isInsideBoundaries = false;
            if (OnExitBoundaries != null)
            {
                OnExitBoundaries();
            }
        }
    }

    public bool CheckInsideBoundaries()
    {
        // Get the user's position from the VR camera transform
        Vector3 userPosition = vrCameraTransform.position;


//boundary variubles 
    float minX = float.PositiveInfinity;
    float maxX = float.NegativeInfinity;
    float minZ = float.PositiveInfinity;
    float maxZ = float.NegativeInfinity;

        // Loop through all the boundary objects and check if the user's position is within their X and Z boundaries
        foreach (GameObject boundaryObject in boundaryObjects)
        {
            // Get the position and scale of the boundary object
            Vector3 boundaryPosition = boundaryObject.transform.position;
            Vector3 boundaryScale = boundaryObject.transform.localScale;

           // Calculate the boundaries
        minX = Mathf.Min(minX, boundaryPosition.x - boundaryScale.x * 0.5f);
        maxX = Mathf.Max(maxX, boundaryPosition.x + boundaryScale.x * 0.5f);
        minZ = Mathf.Min(minZ, boundaryPosition.z - boundaryScale.z * 0.5f);
        maxZ = Mathf.Max(maxZ, boundaryPosition.z + boundaryScale.z * 0.5f);


            // Check if the user's position is inside the boundaries
            if (userPosition.x >= minX && userPosition.x <= maxX && userPosition.z >= minZ && userPosition.z <= maxZ)
            {
                return true;
            }
        }

         // Update UI Text with boundaries
    if (boundariesText != null)
    {
        boundariesText.text = string.Format("X Range: {0} - {1}\nZ Range: {2} - {3}", minX, maxX, minZ, maxZ);
    }


        return false;
    }


  
private void OnCollisionEnter(Collision collision)
{

     Debug.Log("OnCollisionEnter called"); // 
    if (collision.gameObject.CompareTag("Floor"))
    {
         Debug.Log("Collision with floor has been detected"); //
        if (collision.contacts.Length > 0)
        {
            Vector3 contactNormal = collision.contacts[0].normal;
            boundariesText.text = "Contact Normal: " + contactNormal.ToString();

            // Calculate the dot product of the contact normal with the world up vectors
            float dotZ = Vector3.Dot(contactNormal, Vector3.up); // Check if z-axis is up
            float dotX = Vector3.Dot(contactNormal, Vector3.right); // Check if x-axis is up
            float dotY = Vector3.Dot(contactNormal, Vector3.forward); // Check if y-axis is up
        // Play the appropriate sound based on the collision orientation
        if (dotZ >= 0.9f || dotX >= 0.9f) // Z-axis or X-axis is up
        {
            if (collisionAudioSource != null)
            {
                collisionAudioSource.clip = soundForZAndXAxisUp; // Assign the appropriate sound clip
                collisionAudioSource.Play();
            }
        }
        else if (dotY >= 0.9f) // Y-axis is up
        {
            if (collisionAudioSource != null)
            {
                collisionAudioSource.clip = soundForYAxisUp; // Assign the appropriate sound clip
                collisionAudioSource.Play();
            }
        }

        // Notify listeners that a boundary object has collided with the floor
        if (OnBoundaryObjectCollided != null)
        {
            OnBoundaryObjectCollided(gameObject);
        }
    }
}


}

private void OnDrawGizmos()
{
    Gizmos.color = Color.red;

    foreach (GameObject boundaryObject in boundaryObjects)
    {
        Collider collider = boundaryObject.GetComponent<Collider>();

        if (collider != null)
        {
            Gizmos.DrawWireCube(collider.bounds.center, collider.bounds.size);
        }
    }
}

}
