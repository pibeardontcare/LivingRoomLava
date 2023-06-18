using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPrize : MonoBehaviour
{
   
public GameObject prefab;
public Transform startingPoint;
public Transform stoppingPoint;

private GameObject instantiatedPrefab;

public void TriggerMethod()
{
    if (instantiatedPrefab == null)
    {
        // Instantiate the prefab at the startingPoint's position
        instantiatedPrefab = Instantiate(prefab, startingPoint.position, Quaternion.identity);

        // Start the coroutine to move and rotate the prefab
        StartCoroutine(MovePrefab(instantiatedPrefab.transform));
    }
}

private IEnumerator MovePrefab(Transform transform)
{
    float elapsedTime = 0f;
    float totalTime = .8f;

    Vector3 startPosition = transform.position;
    Vector3 endPosition = stoppingPoint.position;
    Quaternion startRotation = transform.rotation;
    Quaternion endRotation = Quaternion.LookRotation(Vector3.forward, endPosition - startPosition);
    Vector3 startScale = transform.localScale;
    Vector3 endScale = startScale * 2f;

    while (elapsedTime < totalTime)
    {
        elapsedTime += Time.deltaTime;

        float t = elapsedTime / totalTime;

        // Lerp the position, rotation, and scale over time
        transform.position = Vector3.Lerp(startPosition, endPosition, t) + new Vector3(Random.Range(0f, 0f), 0, Random.Range(0f, 0f));
        transform.rotation = Quaternion.Slerp(startRotation, endRotation, t);
        transform.localScale = Vector3.Lerp(startScale, endScale, t);

        yield return null;
    }

    // Set the final position, rotation, and scale
    transform.position = endPosition;
    transform.rotation = endRotation;
    transform.localScale = endScale;

    // Set instantiatedPrefab to null so a new instance can be created
    instantiatedPrefab = null;
}
}