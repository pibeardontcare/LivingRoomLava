using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InOrOutColor : MonoBehaviour
{

    public SafeAreaRecorder safeAreaRecorder;

    public Material insideSafeAreaMaterial;
    public Material outsideSafeAreaMaterial;

    private Renderer objectRenderer;

    public Text countdownText;
    public float countdownDuration = 3.0f;
    private bool countingDown = false;

    public bool gameOver = false;

       // Declare an event for game over
    public event System.Action GameOverEvent;

    private void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        countdownText.gameObject.SetActive(false);
    }

    private void Update()
    {
        bool isInsideSafeArea = safeAreaRecorder.isInsideAnyObject;

        if (isInsideSafeArea)
        {
            objectRenderer.material = insideSafeAreaMaterial;
      

            // Reset countdown if the player is inside the safe area
            ResetCountdown();
        }
        else
        {
            objectRenderer.material = outsideSafeAreaMaterial;
            

            if (!countingDown)
            {
                StartCountdown();
            }
        }

        // Check if the game is over
        if (gameOver)
        {

            // Trigger the game over event
            if (GameOverEvent != null)
            {
                GameOverEvent.Invoke();
            }
        }
    }

    private void StartCountdown()
    {
        countdownText.gameObject.SetActive(true);
        countingDown = true;
        StartCoroutine(CountdownCoroutine());
    }

    private void ResetCountdown()
    {
        countingDown = false;
        countdownText.gameObject.SetActive(false);
        countdownText.text = "";
    }

    private IEnumerator CountdownCoroutine()
    {
        float timeLeft = countdownDuration;

        while (timeLeft > 0)
        {
            countdownText.text = Mathf.Ceil(timeLeft).ToString();
            yield return new WaitForSeconds(1.0f);
            timeLeft -= 1.0f;
        }

        countdownText.text = "Game Over";
        gameOver = true;

     //add game is over return to start here   }
    }
}
