using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class paddle : MonoBehaviour
{
    

    public float upwardForce = 1.0f;

    void OnCollisionEnter(Collision collision)
    {
        // Check if the colliding object has a Rigidbody
        Rigidbody otherRigidbody = collision.collider.GetComponent<Rigidbody>();

        if (otherRigidbody != null)
        {
            // Apply upward force to the colliding object
            Vector3 force = new Vector3(0.0f, upwardForce, 0.0f);
            otherRigidbody.AddForce(force, ForceMode.Impulse);
        }
    }
}

