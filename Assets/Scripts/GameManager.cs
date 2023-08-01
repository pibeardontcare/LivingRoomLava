using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject vrHeadsetReference;
    public Collider floorCollider;
    public GameObject signFace;
    public GameObject startArea;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }


     // This array stores the completion status of each level.
    private bool[] levelCompletionStatus;

    // Call this method when a level is completed.
    public void MarkLevelCompleted(int levelIndex)
    {
        levelCompletionStatus[levelIndex] = true;
    }

    // Call this method to check if a level is completed.
    public bool IsLevelCompleted(int levelIndex)
    {
        return levelCompletionStatus[levelIndex];
    }

    // Initialize the level completion status array (e.g., in Start method).
    private void InitializeLevelCompletionStatus()
    {
        levelCompletionStatus = new bool[totalNumberOfLevels];
        // You may set all elements to false initially since no levels are completed at the start.
    }


      // Save the level completion status to PlayerPrefs.
    private void SavePlayerProgress()
    {
        for (int i = 0; i < totalNumberOfLevels; i++)
        {
            PlayerPrefs.SetInt("Level_" + i.ToString(), levelCompletionStatus[i] ? 1 : 0);
        }
        PlayerPrefs.Save();
    }

    // Load the level completion status from PlayerPrefs.
    private void LoadPlayerProgress()
    {
        for (int i = 0; i < totalNumberOfLevels; i++)
        {
            levelCompletionStatus[i] = PlayerPrefs.GetInt("Level_" + i.ToString(), 0) == 1;
        }
    }

     // Call this method when "New Game" button is pressed.
    public void StartNewGame()
    {
        gameManager.InitializeLevelCompletionStatus();
        // Any other necessary initialization for a new game.
    }

    // Call this method when "Continue Saved Game" button is pressed.
    public void ContinueSavedGame()
    {
        gameManager.LoadPlayerProgress();
        // Load the player's position, inventory, or any other relevant data if needed.
    }
}


