using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  // import the UnityEngine.UI namespace
using UnityEngine.EventSystems;

public class RayButtonClick : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Button button;

    public void OnPointerEnter(PointerEventData eventData)
    {
        button.interactable = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        button.interactable = false;
    }
}
