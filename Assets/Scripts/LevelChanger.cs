using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    public Animator animator;
    public Collider paintCollider;

    // This method is called when another collider enters the trigger collider.
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("paint"))
        {
            FadeToLevel(1);
        }
    }

    public void FadeToLevel(int levelIndex)
    {
        animator.SetTrigger("FadeOut");
        float delay = animator.GetCurrentAnimatorStateInfo(0).length;
        Invoke("LoadLevelAfterDelay", delay);
    }

    void LoadLevelAfterDelay()
    {
        int levelIndex = 1; // Change this to the desired level index
        SceneManager.LoadScene(levelIndex);
    }
}