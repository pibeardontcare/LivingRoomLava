using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowBounce : MonoBehaviour
{
    public float bounceHeight = 0.05f; // Height of the bounce
    public float bounceSpeed = 0.5f; // Speed of the bounce
    private float originalY; // Initial y position
    private float timeElapsed = 0f; // Time elapsed since the last bounce

    // Start is called before the first frame update
    void Start()
    {
        originalY = transform.position.y; // Store the initial y position
    }

    // Update is called once per frame
    void Update()
    {
        // Increment time elapsed
        timeElapsed += Time.deltaTime;

        // Check if 2 seconds have passed since the last bounce
        if (timeElapsed >= 2f)
        {
            // Reset time elapsed
            timeElapsed = 0f;

            // Calculate the new y position using a smooth bounce curve (Mathf.Sin)
            float newY = originalY + Mathf.Sin(Time.time * bounceSpeed) * bounceHeight;

            // Update the object's position with the new y value
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        }
    }
}
