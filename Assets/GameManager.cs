using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    // Singleton pattern to have a single instance of GameManager throughout the game.
    public static GameManager instance;

    // Track the current level progress. 0 means no levels complete.
   
    private int levelProgress = 0;

    public TextMeshProUGUI levelText; 

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        if (PlayerPrefs.HasKey("LevelProgress"))
        {
            levelProgress = PlayerPrefs.GetInt("LevelProgress");
        }
        else
        {
            levelProgress = 0;
            PlayerPrefs.SetInt("LevelProgress", levelProgress);
            PlayerPrefs.Save();
        }
    }

    // Update the level number on the UI Text object
    void UpdateLevelText()
    {
        if (levelText != null)
        {
            levelText.text =  levelProgress.ToString();
        }
    }

    // Call this method when a level is completed to update the progress.
    public void LevelCompleted()
    {
        levelProgress++;
        PlayerPrefs.SetInt("LevelProgress", levelProgress);
        PlayerPrefs.Save();
        UpdateLevelText(); // Update the UI Text
    }

    // Call this method to get the current level progress.
    public int GetLevelProgress()
    {
        return levelProgress;
    }

    public void ResetLevelProgress()
    {
        levelProgress = 0;
        PlayerPrefs.SetInt("LevelProgress", levelProgress);
        PlayerPrefs.Save();
        UpdateLevelText(); // Update the UI Text
    }

    private void Start()
    {
        UpdateLevelText(); // Initialize the UI Text with the current level number
    }
}