using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public GameObject[] levelButtonsWithImages; // An array of buttons, each containing the button and prize image

    void Start()
    {
        // Iterate through all levels
        for (int i = 0; i < levelButtonsWithImages.Length; i++)
        {
            int levelCompleted = PlayerPrefs.GetInt("Level" + i + "Completed");
            
            // Check if the level is completed
            if (levelCompleted == 1)
            {
                // Enable the button and show the prize image for the completed level
                levelButtonsWithImages[i].SetActive(true);
            }
        }
    }

    void Update()
    {
        // Your update code here
    }
}
