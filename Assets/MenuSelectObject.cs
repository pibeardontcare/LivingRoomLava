using UnityEngine;

public class MenuSelectObject : MonoBehaviour
{
    public Material hoverMaterial; // Material to apply when hovered
    private Material originalMaterial; // Original material of the object

    void Start()
    {
        originalMaterial = GetComponent<Renderer>().material; // Assuming the object has a Renderer component
    }

    // Function to apply hover material
    public void ApplyHoverMaterial()
    {
        if (hoverMaterial != null)
        {
            GetComponent<Renderer>().material = hoverMaterial;
        }
    }

    // Function to reset material to original
    public void ResetMaterial()
    {
        GetComponent<Renderer>().material = originalMaterial;
    }
}
