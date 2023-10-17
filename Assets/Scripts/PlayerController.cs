using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Import SceneManager

public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform resetTransform;
    [SerializeField] GameObject player;
    [SerializeField] Camera oculusCamera;

    private void Start()
    {
        // Check the level progression and scene name
        if (SceneManager.GetActiveScene().name == "YourSceneName" && YourLevelProgressionCheck() > 0)
        {
            ResetPosition();
        }
    }


    private int YourLevelProgressionCheck()
    {
       //check player level
        return PlayerPrefs.GetInt("LevelProgression", 0);
    }

    public void ResetPosition()
    {
        var rotationAngleY = 180;
        player.transform.Rotate(0, rotationAngleY, 0);

        var distanceDiff = resetTransform.position - oculusCamera.transform.position;

        player.transform.position += distanceDiff;

        Debug.Log("reset player called");
    }
}
