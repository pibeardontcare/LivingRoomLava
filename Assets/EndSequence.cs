using UnityEngine;

public class TriggerScript : MonoBehaviour
{
    public Animator animator; // Drag and drop the Animator component from the Inspector.
    public ParticleSystem particleEmitter; // Drag and drop the Particle System component from the Inspector.

    private void OnTriggerEnter(Collider other)
    {
        // Check if the entering object is the TargetObject.
        if (other.gameObject.CompareTag("Player")) // Make sure to set a unique tag for your TargetObject.
        {
            // Play the animation.
            if (animator != null)
            {
                animator.SetTrigger("YourAnimationTriggerName");
            }

            // Activate the particle emitter.
            if (particleEmitter != null)
            {
                particleEmitter.Play();
            }
        }
    }
}
