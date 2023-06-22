using UnityEngine;
using Oculus.Platform;
using Oculus.Platform.Models;

public class lavaFloorSpawner : MonoBehaviour
{
    public GameObject floorPrefab; // Reference to the floor prefab
    private OVRBoundary.BoundaryType boundaryType = OVRBoundary.BoundaryType.PlayArea;

    private void Start()
    {
        if (OVRManager.boundary.GetConfigured())
        {
            Vector3[] boundaryPoints = OVRManager.boundary.GetGeometry(boundaryType);

            if (boundaryPoints != null && boundaryPoints.Length > 0)
            {
                Vector3 minPoint = boundaryPoints[0];
                Vector3 maxPoint = boundaryPoints[0];

                // Find the minimum and maximum x and z coordinates
                for (int i = 1; i < boundaryPoints.Length; i++)
                {
                    minPoint = Vector3.Min(minPoint, boundaryPoints[i]);
                    maxPoint = Vector3.Max(maxPoint, boundaryPoints[i]);
                }

                // Calculate the center position of the boundary
                Vector3 centerPosition = (minPoint + maxPoint) * 0.5f;

                // Calculate the size of the boundary
                Vector3 boundarySize = maxPoint - minPoint;

                // Instantiate the floor prefab at the center position
                GameObject floor = Instantiate(floorPrefab, centerPosition, Quaternion.identity);

                // Scale the floor prefab to match the size of the play area
                floor.transform.localScale = new Vector3(boundarySize.x, 1f, boundarySize.z);
            }
        }
    }
}
