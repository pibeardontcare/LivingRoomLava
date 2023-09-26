using System.Collections;
using UnityEngine;

public class IndicatorMovement : MonoBehaviour
{
    public Transform target; // The VR headset camera transform (player position)
    public float moveSpeed = 2.0f; // Speed at which the object moves
    public float stopDistance = 0.5f; // Distance to stop from the player
    public float bounceHeight = 0.2f; // Bounce height
    public AudioClip audioClip; // Audio clip to play

    private bool isMoving = true;

    private void Start()
    {
        // Start moving the object towards the player
        StartCoroutine(MoveTowardsPlayer());
    }

    private IEnumerator MoveTowardsPlayer()
    {
        Vector3 initialPosition = transform.position; // Store the initial position

        while (isMoving)
        {
            // Calculate the direction to the player
            Vector3 directionToPlayer = (target.position + target.forward * stopDistance) - transform.position;

            // If close enough, stop moving
            if (directionToPlayer.magnitude <= stopDistance)
            {
                isMoving = false;
                // Play the audio clip
                AudioSource audioSource = GetComponent<AudioSource>();
                audioSource.clip = audioClip;
                audioSource.Play();
            }
            else
            {
                // Move the object towards the player with the added offset
                transform.Translate(directionToPlayer.normalized * moveSpeed * Time.deltaTime);

                // Interpolate the Y position towards 1 over time
                float t = Mathf.Clamp01((initialPosition.y - 1) / (initialPosition.y - transform.position.y));
                float newY = Mathf.Lerp(initialPosition.y, 1, t);
                transform.position = new Vector3(transform.position.x, newY, transform.position.z);

                // Apply a bouncing effect (change Y position)
                float bounceY = Mathf.Sin(Time.time * 5) * bounceHeight;
                transform.position = new Vector3(transform.position.x, transform.position.y + bounceY, transform.position.z);
            }

            yield return null;
        }
    }
}
