using System.Collections.Generic;
using UnityEngine;

public class LevelChanger : MonoBehaviour
{
    public Animator animator;

    // This method is called when another collider enters the trigger collider.
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("paint")) // Replace "YourTag" with the tag of the object entering the trigger.
        {
            FadeToLevel(1); // Replace 1 with the index of the level you want to fade to.
        }
    }

    public void FadeToLevel(int levelIndex)
    {
        animator.SetTrigger("FadeOut");

    }
}
