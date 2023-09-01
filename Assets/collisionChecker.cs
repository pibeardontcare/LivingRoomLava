
using UnityEngine;
using System.Collections.Generic;

public class collisionChecker : MonoBehaviour

   
{
    private HashSet<GameObject> collidedObjects = new HashSet<GameObject>();

    private void OnCollisionEnter(Collision collision)
    {
        GameObject collidingObject = collision.gameObject;

        // Check if the colliding object is already in the set
        if (!collidedObjects.Contains(collidingObject))
        {
            collidedObjects.Add(collidingObject);
            Debug.Log("Collision detected with: " + collidingObject.name);
        }
    }
}
