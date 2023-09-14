using System.Collections;
using UnityEngine;

public class PrizeReveal : MonoBehaviour
{
    public ColorChanger colorChanger; // Drag your ColorChanger script here
    public GameObject prizeDescriptionUI;

    public ParticleSystem particleEmitter; // Drag your particle emitter here
    public Animator endSequenceAnimator; // Drag your end sequence animator here

    [SerializeField] private Animator prizeBox;
    public Transform playerCameraTransform; // Drag your VR Camera transform here

    private bool triggerEntered = false; // Flag to track if the trigger has been entered

    private void Start()
    {
        // Deactivate the particle emitter at the start
        if (particleEmitter != null)
        {
            particleEmitter.Stop();
        }
        prizeBox.SetBool("PrizeReveal", false);
    }

    private IEnumerator PlayEndSequenceWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Start end sequence animation
        if (endSequenceAnimator != null)
        {
            endSequenceAnimator.SetTrigger("StartEndSequence");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !colorChanger.IsGameOver)
        {
            // Check if the player's camera transform enters the trigger
            Vector3 cameraPosition = playerCameraTransform.position;
            if (IsPointInsideTriggerZone(cameraPosition))
            {
                prizeBox.SetBool("PrizeReveal", true);
                // Enable prize description UI and next level button if it's not already active
                if (!prizeDescriptionUI.activeSelf)
                {
                    prizeDescriptionUI.SetActive(true);
                }

                // Start particle emitter
                if (!triggerEntered && particleEmitter != null)
                {
                    particleEmitter.Play();
                    triggerEntered = true;
                }

                // Delay before starting the end sequence animation
                float delayBeforeEndSequence = 2.0f; // Adjust the delay time as needed
                StartCoroutine(PlayEndSequenceWithDelay(delayBeforeEndSequence));
            }
        }
    }

    // Check if a point is inside the trigger zone
    private bool IsPointInsideTriggerZone(Vector3 point)
    {
        Collider triggerCollider = GetComponent<Collider>();

        if (triggerCollider != null)
        {
            return triggerCollider.bounds.Contains(point);
        }

        return false;
    }
}
