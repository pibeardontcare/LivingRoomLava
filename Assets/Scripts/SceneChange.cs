using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    private Dictionary<GameObject, Material> originalMaterialsDict = new Dictionary<GameObject, Material>();
    public Material otherMaterial;  // Assign the material for hover state in the Inspector

    [SerializeField]
    private string sceneName = "3DPaint";

    void Start()
    {
        // Store the original materials for all child objects
        Renderer[] childRenderers = GetComponentsInChildren<Renderer>(true);

        foreach (Renderer childRenderer in childRenderers)
        {
            originalMaterialsDict[childRenderer.gameObject] = childRenderer.sharedMaterial;
        }
    }

    public void HoverOver()
    {
        // Change materials to the hover material for all child objects
        foreach (var kvp in originalMaterialsDict)
        {
            SetMaterial(kvp.Key, otherMaterial);
        }

        // Optionally, add a delay or condition here before loading the scene
        StartCoroutine(LoadSceneAfterDelay());
    }

    public void HoverExit()
    {
        // Revert to the original materials for all child objects when the hover state is exited
        foreach (var kvp in originalMaterialsDict)
        {
            SetMaterial(kvp.Key, kvp.Value);
        }
    }

    // Helper method to set a single material for both Renderer and MeshRenderer of a specific game object
    private void SetMaterial(GameObject obj, Material material)
    {
        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer != null)
        {
            // If the object has a Renderer component
            renderer.sharedMaterial = material;
        }
    }

    // Helper method for loading the scene after a delay
    private IEnumerator LoadSceneAfterDelay()
    {
        yield return new WaitForSeconds(1.0f); // Adjust the delay as needed
        SceneManager.LoadScene(sceneName);
    }
}
