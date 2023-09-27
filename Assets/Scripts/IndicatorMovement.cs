using System.Collections;
using UnityEngine;

public class IndicatorMovement : MonoBehaviour
{
    public Transform target; // The VR headset camera transform (player position)
    public float rotationSpeed = 20.0f; // Speed of rotation towards the player
    public float stopDistance = 0.5f; // Distance to stop from the player
    public float bounceHeight = 0.2f; // Bounce height
    public AudioClip audioClip; // Audio clip to play

    private bool isMoving = true;

    private void Start()
    {
        // Start the coroutine to handle bouncing and rotation
        StartCoroutine(BounceAndRotate());
    }

    private IEnumerator BounceAndRotate()
    {
        Vector3 initialPosition = transform.position; // Store the initial position

        while (isMoving)
        {
            // Calculate the direction to the player
            Vector3 directionToPlayer = (target.position - transform.position);

            // Rotate towards the player
            Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            // If close enough, stop moving
            if (directionToPlayer.magnitude <= stopDistance)
            {
                isMoving = false;
                // Play the audio clip
                AudioSource audioSource = GetComponent<AudioSource>();
                audioSource.clip = audioClip;
                audioSource.Play();
            }

            // Apply a bouncing effect (change Y position)
            float bounceY = Mathf.Sin(Time.time * 5) * bounceHeight;
            transform.position = new Vector3(transform.position.x, initialPosition.y + bounceY, transform.position.z);

            yield return null;
        }
    }
}
