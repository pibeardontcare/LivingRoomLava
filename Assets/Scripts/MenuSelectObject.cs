using UnityEngine;

public class MenuSelectObject : MonoBehaviour
{
    public Material hoverMaterial; // Material to apply when collided
    private Material[] originalMaterials; // Original materials of the objects
    private Renderer[] objectRenderers; // Renderers of the objects

    public GameObject[] objectsToChangeColor; // Array of objects to change color

    void Start()
    {
        // Initialize arrays
        objectRenderers = new Renderer[objectsToChangeColor.Length];
        originalMaterials = new Material[objectsToChangeColor.Length];

        // Store original materials for specified objects (only needs to be done once)
        for (int i = 0; i < objectsToChangeColor.Length; i++)
        {
            objectRenderers[i] = objectsToChangeColor[i].GetComponent<Renderer>();

            if (objectRenderers[i] != null)
            {
                originalMaterials[i] = objectRenderers[i].material;
            }
            else
            {
                Debug.LogError("Renderer component not found on object " + i);
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the collision is with the specified collider
        if (collision.gameObject.CompareTag("paint"))
        {
            ApplyHoverMaterialToSpecified();
            Debug.Log("paintbrush hit");
        }
    }

    void OnCollisionExit(Collision collision)
    {
        // Check if the collision is with the specified collider
        if (collision.gameObject.CompareTag("paint"))
        {
            ResetMaterialToOriginalSpecified();
            Debug.Log("paintbrush exit");
        }
    }

    private void ApplyHoverMaterialToSpecified()
    {
        for (int i = 0; i < objectRenderers.Length; i++)
        {
            if (objectRenderers[i] != null && hoverMaterial != null)
            {
                objectRenderers[i].material = hoverMaterial;
            }
        }
    }

    private void ResetMaterialToOriginalSpecified()
    {
        for (int i = 0; i < objectRenderers.Length; i++)
        {
            if (objectRenderers[i] != null)
            {
                objectRenderers[i].material = originalMaterials[i];
            }
        }
    }
}
