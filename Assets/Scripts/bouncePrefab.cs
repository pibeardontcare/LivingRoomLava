using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bouncePrefab : MonoBehaviour
{

    public float amplitude = 0.1f;
    public float period = 1.0f;

    private float currentTime;
    private Transform cameraTransform;

    // Start is called before the first frame update
    void Start()
    {
        cameraTransform = Camera.main.transform;

    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;

        float y = amplitude * Mathf.Sin(2 * Mathf.PI * currentTime / period);
        transform.localPosition = new Vector3(transform.localPosition.x, y, transform.localPosition.z);
        transform.LookAt(cameraTransform);

    }
}



