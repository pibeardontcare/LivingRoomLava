using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OculusSampleFramework;

public class BounceObject : MonoBehaviour
{
    public float amplitude = 0.5f; // The amplitude of the bounce
    public float frequency = 1f; // The frequency of the bounce
    private Vector3 startPos; // The starting position of the object
    // private Rigidbody rb; // The Rigidbody component of the object
    // private Collider collider; // The Collider component of the object

    void Start()
    {
        startPos = transform.position;
        // rb = GetComponent<Rigidbody>();
        // collider = GetComponent<Collider>();
    }

    void Update()
    {
         transform.position = startPos + amplitude * Mathf.Sin(frequency * Time.time) * Vector3.up;
        // if (!rb.isKinematic)
        // {
           
        // }
    }

    // public void OnGrab()
    // {
    //     rb.isKinematic = true;
    //     collider.enabled = false;
    // }

    // public void OnRelease()
    // {
    //     rb.isKinematic = false;
    //     collider.enabled = true;
    // }
}
