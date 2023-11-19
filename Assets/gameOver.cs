using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameOverHandler : MonoBehaviour
{
    public InOrOutColor inOrOutColor;// Reference to the script controlling game over status
    public GameObject startCouch; // Reference to the object you want to disappear
    public GameObject gameOverCouch; // Reference to the object you want to reappear
    public Light sceneLight; // Reference to the main light in your scene

    private void Start()
    {
        // Ensure the references are set
        if (inOrOutColor == null || startCouch == null || gameOverCouch == null || sceneLight == null)
        {
            Debug.LogError("References not set. Please assign all necessary references in the inspector.");
            enabled = false; // Disable the script if references are not set
        }

        // Subscribe to the game over event
        inOrOutColor.GameOverEvent += OnGameOver;
    }

    private void OnGameOver()
    {
        // Change lighting when the game is over (you can customize this part based on your requirements)
        sceneLight.color = Color.red;
        sceneLight.intensity = 0.5f;

        // Make one object disappear and the other reappear
        startCouch.SetActive(false);
        gameOverCouch.SetActive(true);
        gameOverCouch.transform.position = new Vector3(0, 0.1f, 0.5f);
    }

    // Ensure to unsubscribe from events when the script is disabled or destroyed
    private void OnDisable()
    {
        if (inOrOutColor != null)
        {
        inOrOutColor.GameOverEvent -= OnGameOver;
        }
    }
}
