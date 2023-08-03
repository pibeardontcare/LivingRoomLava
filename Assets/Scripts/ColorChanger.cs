using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorChanger : MonoBehaviour
{
    public Material materialInsideStartObject;
    public Material materialInsideBoundaries;
    public Material materialOutsideAllBoundaries;
    public GameObject startObject;
    public float countdownDuration = 3.0f; // Countdown duration in seconds
    public Text countdownText; // Reference to the UI text for countdown
    public Text gameOverText; // Reference to the UI text for game over message
    public bool IsGameOver { get; private set; } = false; // Add a public property to indicate if the game is over

    private Renderer objectRenderer;
    private BoundaryChecker boundaryChecker;
    private bool isCountingDown = false;
    private float countdownTimer;

    private void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        boundaryChecker = GameObject.FindObjectOfType<BoundaryChecker>();
        countdownTimer = countdownDuration;
    }

    private void Update()
    {
        bool isInsideBoundaries = boundaryChecker.CheckInsideBoundaries();
        bool isInsideStartObject = IsInsideStartObject();

        if (isInsideStartObject)
        {
            objectRenderer.material = materialInsideStartObject;
            ResetCountdown();
        }
        else if (isInsideBoundaries)
        {
            objectRenderer.material = materialInsideBoundaries;
            ResetCountdown();
        }
        else
        {
            objectRenderer.material = materialOutsideAllBoundaries;
            StartCountdown();
        }

        UpdateCountdown();

    }
        // Check if the user's position is inside the boundaries of the start object
    private bool IsInsideStartObject()
    {
        // Get the position and scale of the start object
        Vector3 startObjectPosition = startObject.transform.position;
        Vector3 startObjectScale = startObject.transform.localScale;

        // Calculate the half extents of the start object
        float halfStartObjectX = startObjectScale.x * 0.5f;
        float halfStartObjectZ = startObjectScale.z * 0.5f;

        // Get the user's position (VR headset camera position)
        Vector3 userPosition = Camera.main.transform.position;

        // Check if the user's position is within the boundaries of the start object
        if (userPosition.x >= startObjectPosition.x - halfStartObjectX && userPosition.x <= startObjectPosition.x + halfStartObjectX &&
            userPosition.z >= startObjectPosition.z - halfStartObjectZ && userPosition.z <= startObjectPosition.z + halfStartObjectZ)
        {
            return true; // User's position is inside the boundaries of the start object
        }

        return false; // User's position is outside the boundaries of the start object
    }

   

    private void StartCountdown()
    {
        if (!isCountingDown)
        {
            isCountingDown = true;
            countdownTimer = countdownDuration;
        }
    }

    private void UpdateCountdown()
    {
        if (isCountingDown)
        {
            countdownTimer -= Time.deltaTime;
            countdownText.text = Mathf.CeilToInt(countdownTimer).ToString();

            if (countdownTimer <= 0)
            {
                GameOver();
            }
        }
    }

    private void ResetCountdown()
    {
        isCountingDown = false;
        countdownTimer = countdownDuration;
        countdownText.text = "";
    }

    private void GameOver()
    {
        IsGameOver = true; // Set the IsGameOver flag to true
        gameOverText.gameObject.SetActive(true);
    }
}

