using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerIncrement : MonoBehaviour
{
    public GameObject fireworks;
    // Start is called before the first frame update
    private void onTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "prize")
        {
            //Trigger fireworks
            Debug.Log("prize triggered trigger");
            Instantiate(fireworks, transform.position, transform.rotation);

        }
    }

 
}
