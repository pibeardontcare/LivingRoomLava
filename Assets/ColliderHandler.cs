using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Disable the collider at the start of the game
        GetComponent<Collider>().enabled = false;
        Debug.Log("collider NOT ready!");
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
