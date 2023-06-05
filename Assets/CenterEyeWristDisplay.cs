using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CenterEyeWristDisplay : MonoBehaviour
{
   public Transform centerEyeAnchor;
    public Transform leftHand;

    private Canvas canvas;
    private Text text;

    private void Start()
    {
        canvas = gameObject.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.WorldSpace;
        canvas.GetComponent<RectTransform>().sizeDelta = new Vector2(200, 50);
        canvas.gameObject.AddComponent<CanvasScaler>();


        text = gameObject.AddComponent<Text>();
        text.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        text.color = Color.black;
        text.alignment = TextAnchor.MiddleCenter;
        text.text = "Center Eye Anchor Position: ";
    }

    private void Update()
    {
        canvas.transform.position = leftHand.position;
        canvas.transform.rotation = Quaternion.LookRotation(centerEyeAnchor.GetComponent<Camera>().transform.forward, centerEyeAnchor.GetComponent<Camera>().transform.up);

        text.text = "Center Eye Anchor Position: " + centerEyeAnchor.position;
    }
}

