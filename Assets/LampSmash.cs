using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampSmash : MonoBehaviour
{
    public AudioClip collisionSound; // The sound to play on collision
    private bool hasCollided = false; // To prevent multiple collision sound plays

    private void OnCollisionEnter(Collision collision)
    {
        if (!hasCollided && collision.gameObject.CompareTag("Floor"))
        {
            AudioSource.PlayClipAtPoint(collisionSound, transform.position);
            hasCollided = true;
        }
    }
}
