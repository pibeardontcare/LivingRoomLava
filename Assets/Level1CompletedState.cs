using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public GameObject[] levelButtons; // An array of buttons for each level
    public GameObject[] prizeImages; // An array of prize images for each level

    void Start()
    {
        // Iterate through all levels
        for (int i = 0; i < levelButtons.Length; i++)
        {
            int levelCompleted = PlayerPrefs.GetInt("Level" + i + "Completed");
            
            // Check if the level is completed
            if (levelCompleted == 1)
            {
                // Enable the button and show the prize image for the completed level
                levelButtons[i].SetActive(true);
                prizeImages[i].SetActive(true);
            }
        }
    }

    void Update()
    {
        // Your update code here
    }
}
