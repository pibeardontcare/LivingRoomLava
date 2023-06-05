using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveLava : MonoBehaviour
{
   public float speed = 1.0f;
    private float direction = 1.0f;
    private float limit = 0.1f;

    void Update()
    {
        transform.position += new Vector3(direction * speed * Time.deltaTime, 0, 0);
        if (transform.position.x > limit || transform.position.x < -limit)
        {
            direction = -direction;
        }
    }
}
