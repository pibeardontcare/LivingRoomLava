using System.Collections;
using UnityEngine;

public class PrizeReveal : MonoBehaviour
{
    public ColorChanger colorChanger; // Drag your ColorChanger script here
    public GameObject prizeDescriptionUI;

    public ParticleSystem particleEmitter; // Drag your particle emitter here
    public Animator endSequenceAnimator; // Drag your end sequence animator here

    [SerializeField] private Animator prizeBox;
    private bool triggerEntered = false; // Flag to track if the trigger has been entered

    private void Start()
    {
        // Deactivate the particle emitter at the start
        if (particleEmitter != null)
        {
            particleEmitter.Stop();
        }
        prizeBox.SetBool("PrizeReveal", false); // Fixed missing semicolon and added the missing parameter
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
