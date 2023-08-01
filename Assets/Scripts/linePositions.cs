using UnityEngine;

public class LinePositions : MonoBehaviour
{
    public float outlineHeight = 0.1f;
    public Collider linkedCollider; // Public field for the linked collider
    public Transform prefabTransform; // Public field for the prefab's transform

    private LineRenderer lineRenderer;

    private void Start()
    {
        // Get the LineRenderer component or add one if it doesn't exist
        lineRenderer = GetComponent<LineRenderer>();
        if (lineRenderer == null)
            lineRenderer = gameObject.AddComponent<LineRenderer>();

        // Configure the LineRenderer properties
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.widthMultiplier = 0.1f; // Set the width of the line
        lineRenderer.startColor = Color.white;
        lineRenderer.endColor = Color.white;

        // Set the loop of the line renderer to draw a continuous loop around the prefab
        lineRenderer.loop = true;

        // Disable the line renderer initially
        lineRenderer.enabled = false;
    }

    private void UpdateLinePositions()
    {
        // Generate the corners of the prefab in X and Z space
        Vector3 min = prefabTransform.position - prefabTransform.localScale / 2f;
        Vector3 max = prefabTransform.position + prefabTransform.localScale / 2f;

        Vector3[] prefabCorners = new Vector3[8];
        prefabCorners[0] = new Vector3(min.x, outlineHeight, min.z);
        prefabCorners[1] = new Vector3(min.x, outlineHeight, max.z);
        prefabCorners[2] = new Vector3(max.x, outlineHeight, max.z);
        prefabCorners[3] = new Vector3(max.x, outlineHeight, min.z);

        // Set the positions of the line renderer
        lineRenderer.positionCount = prefabCorners.Length;
        lineRenderer.SetPositions(prefabCorners);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the entered collider is the linked collider
        if (other == linkedCollider)
        {
            // Enable the line renderer when the prefab intersects with the linked collider
            lineRenderer.enabled = true;
            UpdateLinePositions();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the exited collider is the linked collider
        if (other == linkedCollider)
        {
            // Disable the line renderer when the prefab is no longer intersecting with the linked collider
            lineRenderer.enabled = false;
        }
    }
}
