using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class balloonConstraint : MonoBehaviour
{

    public GameObject paddle; // Reference to the paddle GameObject
    public float maxYDistance = 3f; // Maximum distance along the y-axis
    public float maxRadius = 1f; // Maximum radius from the center of the paddle's surface

 public float minPosY = 0.8f; // Minimum y-coordinate for the balloon
    void Update()
    {
        // Ensure that the ball stays within the specified limits
        ConstrainBall();
    }
void ConstrainBall()
    {
        // Get the position of the paddle and the ball
        Vector3 paddlePosition = paddle.transform.position;
        Vector3 ballPosition = transform.position;

        // Constrain the ball along the y-axis
        float newY = Mathf.Clamp(ballPosition.y, minPosY, paddlePosition.y + maxYDistance);

        // Constrain the ball within a certain radius from the center of the paddle's surface
        Vector2 paddleSurface = new Vector2(paddlePosition.x, paddlePosition.z);
        Vector2 ballSurface = new Vector2(ballPosition.x, ballPosition.z);

        float distance = Vector2.Distance(paddleSurface, ballSurface);
        if (distance > maxRadius)
        {
            // If the ball is outside the allowed radius, reposition it
            Vector2 direction = (ballSurface - paddleSurface).normalized;
            Vector2 constrainedPosition = paddleSurface + direction * maxRadius;
            transform.position = new Vector3(constrainedPosition.x, newY, constrainedPosition.y);
        }
        else
        {
            // If the ball is within the allowed radius, update its position
            transform.position = new Vector3(ballPosition.x, newY, ballPosition.z);
        }
    }
}