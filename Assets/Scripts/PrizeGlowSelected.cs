using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Oculus;


public class PrizeGlowSelected : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hand")) // Assuming "Hand" is tagged appropriately
        {
            // Hide or deactivate the object when the "Hand" enters the trigger collider
            gameObject.SetActive(false);
            // You can also destroy the object if you don't need it anymore
            // Destroy(gameObject);
        }
    }
}