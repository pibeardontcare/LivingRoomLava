using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonBop : MonoBehaviour
{
   private Rigidbody rb;
    private bool isBouncing = false;

    public float bounceForce = 5.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!isBouncing)
        {
            // Apply a bounce force when colliding with something
            rb.AddForce(Vector3.up * bounceForce, ForceMode.Impulse);
            isBouncing = true;
        }
    }
}
