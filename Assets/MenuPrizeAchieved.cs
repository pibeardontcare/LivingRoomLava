using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MenuPrizeAchieved : MonoBehaviour
{
    // Reference to the GameObject you want to show for level 1 achievement.
    public GameObject level1GameObject;
    public GameObject prizeDescription;

    private void Start()
    {
        // Ensure the UI object is initially hidden
         prizeDescription.SetActive(false);
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

       public void OnButtonClick()
    {
        // Toggle the visibility of the UI object
        prizeDescription.SetActive(!prizeDescription.activeSelf);
    }
}
