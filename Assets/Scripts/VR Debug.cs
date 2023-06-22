using UnityEngine;
using UnityEngine.UI;

public class VRDebug : MonoBehaviour
{

    public GameObject UI;
    public GameObject UIAnchor;
    private bool UIActive;

    // Start is called before the first frame update
    void Start()
    {
        UI.SetActive(false);
        UIActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.Two))
        {
            UIActive = !UIActive;
            UI.SetActive(UIActive);
        }
        if (UIActive)
        {
            UI.transform.position = UIAnchor.transform.position;
            UI.transform.eulerAngles = new Vector3(UIAnchor.transform.eulerAngles.x, UIAnchor.transform.eulerAngles.y, 0);
        }
        if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
        {
                Debug.Log("Right Trigger Pressed");
        }
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
        {
                Debug.Log("Left Trigger Pressed");
        }

    }
}
