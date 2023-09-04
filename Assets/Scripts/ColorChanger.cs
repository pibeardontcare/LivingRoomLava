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
    public AudioSource audioSource; // Reference to the AudioSource component
    public AudioClip fadeInClip; // Audio clip for fading in
    public AudioSource warningSoundSource; // Reference to the warning sound AudioSource
    public AudioClip warningClip; // Warning sound clip
    public Camera playerCamera;

    private bool boundaryObjectCollided = false;
    private Renderer objectRenderer;
    private bool isCountingDown = false;
    private float countdownTimer;
    private float initialVolume; // Initial volume of the audio source
    private bool isFadingOut = false;

    private bool isOutsideSafeArea = false; // Add a flag to track if the player is outside the safe area
    private bool isPlayingWarningSound = false; // Flag to track if the warning sound is playing

    private void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        countdownTimer = countdownDuration;

        // Store the initial volume of the audio source
        initialVolume = audioSource.volume;

        // Make sure Game Over text is initially inactive
        gameOverText.gameObject.SetActive(false);

        // Set IsGameOver to false when the script starts
        IsGameOver = false;
        boundaryObjectCollided = false; // Initialize the flag to false
    }

    private void Update()
    {
        if (playerCamera.transform.position.z <= 0.5f)
            return;

        bool isInsideStartObject = IsInsideStartObject();

        if (isInsideStartObject && !boundaryObjectCollided)
        {
            objectRenderer.material = materialInsideStartObject;
            ResetCountdown();
            isOutsideSafeArea = false; // Reset the flag when inside the start object
        }
        else if (IsInsideBoundaries() && boundaryObjectCollided)
        {
            objectRenderer.material = materialInsideBoundaries;
            ResetCountdown();
            isOutsideSafeArea = false; // Reset the flag when inside the safe boundaries
        }
        else
        {
            objectRenderer.material = materialOutsideAllBoundaries;
            StartCountdown();
            isOutsideSafeArea = true; // Set the flag when outside both safe area and boundaries

            // Check if the player is outside the safe area and the warning sound is not playing
            if (!isPlayingWarningSound)
            {
                PlayWarningSound();
                isPlayingWarningSound = true; // Set the flag when starting to play the warning sound
            }
        }

        UpdateCountdown();

        // Check if the player is returning to the safe/start area
        if ((isInsideStartObject || IsInsideBoundaries()) && isOutsideSafeArea)
        {
            StopWarningSound();
        }
    }
     private void PlayWarningSound()
    {
        if (!warningSoundSource.isPlaying)
        {
            warningSoundSource.clip = warningClip;
            warningSoundSource.Play();
        }
    }

    private void StopWarningSound()
    {
        if (warningSoundSource.isPlaying)
        {
            warningSoundSource.Stop();
            isPlayingWarningSound = false; // Reset the flag when stopping the warning sound
        }
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
        Vector3 userPosition = playerCamera.transform.position;

        // Check if the user's position is within the boundaries of the start object
        if (userPosition.x >= startObjectPosition.x - halfStartObjectX && userPosition.x <= startObjectPosition.x + halfStartObjectX &&
            userPosition.z >= startObjectPosition.z - halfStartObjectZ && userPosition.z <= startObjectPosition.z + halfStartObjectZ)
        {
            return true; // User's position is inside the boundaries of the start object
        }

        return false; // User's position is outside the boundaries of the start object
    }

    private bool IsInsideBoundaries()
    {
        // Implement your logic for checking if the user is inside the boundaries
        // You can use Collider or other methods to check the boundaries
        // Replace the return statement with your implementation
        return false; // Placeholder return
    }

    private void StartCountdown()
    {
        if (!isCountingDown)
        {
            isCountingDown = true;

            countdownTimer = countdownDuration;

            // Start fading out audio
            isFadingOut = true;
            StartCoroutine(FadeOutAudio());
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
        if (isCountingDown)
        {
            // Stop fading out audio and start fading in
            isFadingOut = false;
            FadeInAudio();
        }

        isCountingDown = false;
        countdownTimer = countdownDuration;
        countdownText.text = "";
    }

    private void GameOver()
    {
         if (isFadingOut)
        {
            // Stop fading out audio and start fading in
            isFadingOut = false;
            FadeInAudio();
        }

        IsGameOver = true;
        gameOverText.gameObject.SetActive(true);

        // Hide the countdown timer text
        countdownText.gameObject.SetActive(false);
    }


    private IEnumerator FadeOutAudio()
    {
        float startVolume = audioSource.volume;
        float fadeDuration = 0.5f;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / fadeDuration;
            yield return null;
        }

        audioSource.Pause();
    }

    private void FadeInAudio()
    {
        if (!isFadingOut)
        {
            isFadingOut = true;
            StartCoroutine(FadeInAudioCoroutine());
        }
    }

  private IEnumerator FadeInAudioCoroutine()
{
    audioSource.clip = fadeInClip; // Use fadeInClip instead of fadeClip
    audioSource.volume = 0;
    audioSource.Play();

    float startVolume = audioSource.volume;
    float fadeDuration = 0.5f;

    while (audioSource.volume < startVolume)
    {
        audioSource.volume += startVolume * Time.deltaTime / fadeDuration;
        yield return null;
    }

    audioSource.volume = startVolume;
    isFadingOut = false;
}

}