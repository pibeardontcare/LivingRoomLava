using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampSmash : MonoBehaviour
{
    public AudioClip collisionSound; // The sound to play on collision
    public GameObject brokenObjectPrefab; // Reference to the broken object prefab
    public GameObject smashEffect; // Reference to the GameObject with particle systems
    public float smashDuration = 2.0f; // Duration for which the smash effect plays

    private bool hasCollided = false; // To prevent multiple collision sound plays

    private void OnCollisionEnter(Collision collision)
    {
        if (!hasCollided && collision.gameObject.CompareTag("Lava"))
        {
            // Play collision sound
            AudioSource.PlayClipAtPoint(collisionSound, transform.position);

            // Get the position and rotation of the thrown object
            Vector3 spawnPosition = new Vector3(transform.position.x, -0.136f, transform.position.z);
            Quaternion spawnRotation = Quaternion.Euler(-14.572f, 0f, 0f);

            // Instantiate the broken object at the specified position and rotation
            GameObject brokenObject = Instantiate(brokenObjectPrefab, spawnPosition, spawnRotation);

            // Optionally, you can destroy the original object
            Destroy(gameObject);

            // Activate the particle systems after 'smashDuration' seconds
            StartCoroutine(ActivateSmashEffect());

            hasCollided = true;
        }
    }

    // Coroutine to activate the smash effect after a short delay
    private IEnumerator ActivateSmashEffect()
    {
        yield return new WaitForSeconds(0.1f); // Adjust the delay as needed
        if (smashEffect != null)
        {
            smashEffect.SetActive(true);

            // Disable the particle systems after 'smashDuration' seconds
            StartCoroutine(DisableSmashEffect());
        }
    }

    // Coroutine to disable the particle systems after a specified duration
    private IEnumerator DisableSmashEffect()
    {
        yield return new WaitForSeconds(smashDuration);
        if (smashEffect != null)
        {
            smashEffect.SetActive(false);
        }
    }
}

