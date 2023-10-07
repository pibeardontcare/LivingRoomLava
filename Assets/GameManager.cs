using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Singleton pattern to have a single instance of GameManager throughout the game.
    public static GameManager instance;

    // Track the current level progress. 0 means no levels complete.
    private int levelProgress = 0;

    private void Awake()
    {
        // Ensure there's only one instance of GameManager.
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Keep GameManager between scenes.
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Initialize the level progress to 0 when the menu first loads.
        levelProgress = 0;
    }

    // Call this method when a level is completed to update the progress.
    public void LevelCompleted()
    {
        levelProgress++;
        PlayerPrefs.SetInt("LevelProgress", levelProgress);
        PlayerPrefs.Save();
    }

    // Call this method to get the current level progress.
    public int GetLevelProgress()
    {
        return levelProgress;
    }

    // Reset the level progress (for debugging or menu reset).
    public void ResetLevelProgress()
    {
        levelProgress = 0;
        PlayerPrefs.SetInt("LevelProgress", levelProgress);
        PlayerPrefs.Save();
    }
}
