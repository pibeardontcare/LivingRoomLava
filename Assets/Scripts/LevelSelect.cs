using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    public Image levelImage;
    public Button forwardButton;
    public Button backwardButton;

    private int currentLevelIndex = 0;
    private Vector3 targetPosition;
    private bool isMoving = false;

    private void Start()
    {
        forwardButton.onClick.AddListener(MoveForward);
        backwardButton.onClick.AddListener(MoveBackward);
        UpdateTargetPosition();
    }

    private void Update()
    {
        if (isMoving)
        {
            levelImage.rectTransform.localPosition = Vector3.MoveTowards(
                levelImage.rectTransform.localPosition, targetPosition, Time.deltaTime * 526f);

            if (Vector3.Distance(levelImage.rectTransform.localPosition, targetPosition) < 0.01f)
            {
                isMoving = false;
            }
        }
    }

    private void MoveForward()
    {
        if (currentLevelIndex < GetMaxLevelIndex())
        {
            currentLevelIndex++;
            UpdateTargetPosition();
        }
    }

    private void MoveBackward()
    {
        if (currentLevelIndex > 0)
        {
            currentLevelIndex--;
            UpdateTargetPosition();
        }
    }

    private void UpdateTargetPosition()
    {
        targetPosition = new Vector3(currentLevelIndex * -526f, levelImage.rectTransform.localPosition.y, 0f);
        isMoving = true;
    }

    private int GetMaxLevelIndex()
    {
        // Return the maximum level index based on the number of available levels
        return 2; // For example, if you have levels 1 through 3, the highest index is 2.
    }
}
