
using UnityEngine;
using UnityEngine.UI;

public class FPS : MonoBehaviour
{

    Text fpsText;
    public int refreshRate = 10;
    int frameCounter;
    float totalTime;

    void Start()
    {
        fpsText = GetComponent<Text>();
        frameCounter = 0;
        totalTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (frameCounter == refreshRate)
        {
            float averageFps = (1.0f / (totalTime / refreshRate));
            fpsText.text = averageFps.ToString("F1");
            frameCounter = 0;
            totalTime = 0;
        } else
        {
            totalTime += Time.deltaTime;
            frameCounter++;
        }

    }
}
