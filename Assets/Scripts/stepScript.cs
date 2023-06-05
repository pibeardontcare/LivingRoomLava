using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stepScript : MonoBehaviour
{
    public GameObject objectToGlow;

    private Material objectMaterial;
    // Start is called before the first frame update
    void Start()
    {
        objectMaterial = objectToGlow.GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        
        if(other.CompareTag("Player")){
            // Debug.log("trigger in");
            objectMaterial.EnableKeyword("_EMISSION");
        }
    }

     void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player")){
            // Debug.log("trigger out");
            objectMaterial.DisableKeyword("_EMISSION");
        }
    }
}
