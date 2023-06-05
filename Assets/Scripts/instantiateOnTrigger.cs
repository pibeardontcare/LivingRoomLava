using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class instantiateOnTrigger : MonoBehaviour
{
    public GameObject[] targetObjects;
    public GameObject prefab;
    // Start is called before the first frame update

    void Start()
    {
          if (targetObjects.Length == 0)
        {
            Debug.LogWarning("No target objects to instantiate on");
            return;
        }
        int randomIndex = Random.Range(0, targetObjects.Length);
        GameObject target = targetObjects[randomIndex];
        Instantiate(prefab, target.transform.position, target.transform.rotation);
        prefab.transform.Rotate(new Vector3(0, 270, 0));
    }
    

    

}