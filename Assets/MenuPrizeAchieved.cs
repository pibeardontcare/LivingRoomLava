using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPrizeAchieved : MonoBehaviour
{
    // Reference to the GameObject you want to show for level 1 achievement.
    public GameObject level1GameObject;

    private void Start()
    {
        // Check the level progress to determine if level 1 is achieved.
        int levelProgress = GameManager.instance.GetLevelProgress();

        // Check if level 1 is achieved (assuming level 1 is at index 1).
        if (levelProgress >= 1)
        {
            // Level 1 is achieved, so show the associated GameObject.
            level1GameObject.SetActive(true);
        }
        else
        {
            // Level 1 is not achieved, so hide the GameObject.
            level1GameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
