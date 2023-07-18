using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class SafeXZ
        {
    public static bool IsInXZRange(this Vector3 position, Vector3 targetPosition, float range)
    {
        Vector3 positionXZ = new Vector3(position.x, 0f, position.z);
        Vector3 targetPositionXZ = new Vector3(targetPosition.x, 0f, targetPosition.z);
        return Vector3.Distance(positionXZ, targetPositionXZ) <= range;
    }
} 
   