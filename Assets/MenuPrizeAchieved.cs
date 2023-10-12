using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MenuPrizeAchieved : MonoBehaviour
{
    // Reference to the GameObject you want to show for this level's achievement.
    public GameObject prizeObject;
    public GameObject prizeDescription;

    // Public variable to set the level for this script instance.
    public int level;

    private void Start()
    {
        // Ensure the UI object is initially hidden
        prizeDescription.SetActive(false);
        prizeObject.SetActive(false);

        // Check the level progress to determine if this level is achieved.
        int levelProgress = GameManager.instance.GetLevelProgress();

        // Check if the specified level is achieved.
        if (levelProgress >= level)
        {
            // Level is achieved, so show the associated GameObject.
            prizeObject.SetActive(true);
        }
        else
        {
            // Level is not achieved, so hide the GameObject.
            prizeObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // ...
    }

    public void OnButtonClick()
    {
        // Toggle the visibility of the UI object
        prizeDescription.SetActive(!prizeDescription.activeSelf);
    }
}