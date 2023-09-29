using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class splineHolder : MonoBehaviour
{

   
    public float speed = 2.0f; // Speed at which the object moves along the spline

    private bool isGrabbed = false;
    private float t = 0.0f;

    private void Update()
    {
        if (isGrabbed)
        {
            // Calculate the new position along the spline based on time and speed
            t += Time.deltaTime * speed;

            // Move the object to the calculated position on the spline
            //Vector3 newPosition = spline.GetPointOnSpline(t);
            //transform.position = newPosition;

            // Optionally, update the object's rotation to match the spline's tangent
            //Quaternion newRotation = Quaternion.LookRotation(spline.GetTangentOnSpline(t));
            //transform.rotation = newRotation;

            // Check if the object has reached the end of the spline
            if (t >= 1.0f)
            {
                // Object has reached the end, stop its movement
                isGrabbed = false;
            }
        }
    }

    public void GrabObject()
    {
        // Start moving the object along the spline when it's grabbed
        isGrabbed = true;
    }

    public void ReleaseObject()
    {
        // Stop the object's movement when it's released
        isGrabbed = false;
    }
}
