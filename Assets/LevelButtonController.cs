using UnityEngine;
using UnityEngine.UI;

public class LevelButtonController : MonoBehaviour
{
    public Button levelButton; // Reference to your UI button.
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.instance; // Get a reference to the GameManager instance.

        // Check if level 1 is completed, and enable the button accordingly.
        if (gameManager != null && gameManager.IsLevelCompleted(0))
        {
            levelButton.interactable = true; // Enable the button.
        }
        else
        {
            levelButton.interactable = false; // Disable the button.
        }
    }
}
