using UnityEngine;

public class ColorChangeScript : MonoBehaviour
{
    public GameObject objectToShowHide;
    // Reference to the SafeAreaRecorder script
    public SafeAreaRecorder safeAreaRecorder;

    // The materials for different states
    public Material insideSafeAreaMaterial;
    public Material outsideSafeAreaMaterial;

    // Reference to the object's renderer
    private Renderer objectRenderer;

    private void Start()
    {
        // Get the object's renderer component
        objectRenderer = GetComponent<Renderer>();
    }

    private void Update()
    {
        // Check the value of isInsideAnyObject from the SafeAreaRecorder script
        bool isInsideSafeArea = safeAreaRecorder.isInsideAnyObject;

        // Change the object's material based on the boolean value
        if (isInsideSafeArea)
        {
            objectRenderer.material = insideSafeAreaMaterial;
            objectToShowHide.SetActive(true);
        }
        else
        {
            objectRenderer.material = outsideSafeAreaMaterial;
            objectToShowHide.SetActive(false);
        }
    }
}
