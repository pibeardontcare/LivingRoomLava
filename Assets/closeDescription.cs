using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class closeDescription : MonoBehaviour
{


    public GameObject uiObjectToHide1;
    public GameObject uiObjectToHide2;

    public void OnButtonClick()
        {
        // Hide both UI objects
        uiObjectToHide1.SetActive(false);
        uiObjectToHide2.SetActive(false);
        }
 }
