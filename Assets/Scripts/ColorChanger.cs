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
     public GameObject boundaryCheckerObject;

    private bool boundaryObjectCollided = false;
    private Renderer objectRenderer;
    private BoundaryChecker boundaryChecker;
    private bool isCountingDown = false;
    private float countdownTimer;
    private float initialVolume; // Initial volume of the audio source
    private bool isFadingOut = false;
    
    private void Awake()
{
    // Make sure the boundaryCheckerObject field is assigned in the Inspector
    boundaryChecker = boundaryCheckerObject.GetComponent<BoundaryChecker>();
}

    private void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        boundaryChecker = GameObject.FindObjectOfType<BoundaryChecker>();
        countdownTimer = countdownDuration;

         // Store the initial volume of the audio source
        initialVolume = audioSource.volume;


        // Make sure Game Over text is initially inactive
        gameOverText.gameObject.SetActive(false);

        // Set IsGameOver to false when the script starts
        IsGameOver = false;
        boundaryObjectCollided = false; // Initialize the flag to false
    }
     private void OnEnable()
    {
        boundaryChecker.OnBoundaryObjectCollided += OnBoundaryObjectCollided;
    }

    private void OnDisable()
    {
        boundaryChecker.OnBoundaryObjectCollided -= OnBoundaryObjectCollided;
    }

    private void OnBoundaryObjectCollided(GameObject boundaryObject)
    {
        // This method will be called when a boundary object collides with the floor
        // Update the flag to indicate that a boundary object has collided
        boundaryObjectCollided = true;

        // You can place any logic here that should run when a boundary object is thrown and collides with the floor.
        // For example, you can change colors or perform other actions.
        // You can also start the countdown timer if necessary.
    }
    private void Update()
    {
       if (playerCamera.transform.position.z <= 0.5f)
            return;

    // Check if the user is inside the boundaries before updating colors and countdown
  
        bool isInsideBoundaries = boundaryChecker.CheckInsideBoundaries();
        bool isInsideStartObject = IsInsideStartObject();

        if (isInsideStartObject && !boundaryObjectCollided)
        {
            objectRenderer.material = materialInsideStartObject;
            ResetCountdown();
        }
        else if (isInsideBoundaries && boundaryObjectCollided)
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


        // Check if the player is outside both safe and start areas, then play warning sound
        if (!isInsideStartObject && !isInsideBoundaries && boundaryObjectCollided)
            {
                PlayWarningSound();
            }

        // Check if the player is returning to safe/start area
        if ((isInsideStartObject || isInsideBoundaries) && isFadingOut)
        {
            FadeInAudio();

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
private void PlayWarningSound()
{
    if (!warningSoundSource.isPlaying)
    {
        warningSoundSource.clip = warningClip;
        warningSoundSource.Play();
    }
}
}