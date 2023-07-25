using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabOutline : MonoBehaviour
{
   
    public LineRenderer lineRenderer;
    public LayerMask prefabLayer;

    private void Start()
    {
        lineRenderer.positionCount = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsPrefabObject(other.gameObject))
        {
            DrawLineAroundPrefabsTouchingCollider();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (IsPrefabObject(other.gameObject))
        {
            DrawLineAroundPrefabsTouchingCollider();
        }
    }

    private bool IsPrefabObject(GameObject obj)
    {
        // Check if the object belongs to the prefabLayer
        return (1 << obj.layer & prefabLayer.value) != 0;
    }

    private void DrawLineAroundPrefabsTouchingCollider()
    {
        // Get all prefabs in the scene based on their layer
        GameObject[] prefabs = GameObject.FindGameObjectsWithTag("PrefabTag"); // Replace "PrefabTag" with the tag you assign to your prefabs

        // Calculate the positions for the line renderer
        List<Vector3> linePositions = new List<Vector3>();
        foreach (GameObject prefab in prefabs)
        {
            if (prefab.GetComponent<Collider>().bounds.Intersects(GetComponent<Collider>().bounds))
            {
                Vector3 position = prefab.transform.position;
                Vector3 scale = new Vector3(prefab.transform.localScale.x * 0.5f, 0, prefab.transform.localScale.z * 0.5f);
                linePositions.Add(position + scale);
                linePositions.Add(position + new Vector3(scale.x, 0, -scale.z));
                linePositions.Add(position - scale);
                linePositions.Add(position + new Vector3(-scale.x, 0, scale.z));
                linePositions.Add(position + scale);
            }
        }

        // Set the line renderer positions
        lineRenderer.positionCount = linePositions.Count;
        lineRenderer.SetPositions(linePositions.ToArray());
    }
}
