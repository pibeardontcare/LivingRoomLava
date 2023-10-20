using UnityEngine;
using UnityEngine.SceneManagement;
public class SpawnPointManager : MonoBehaviour
{
    public Transform head; // Reference to spawn point Transform
    public Transform origin; // Reference Oculus main camera
    public Transform target;
    public GameObject objectToShow;

    public Color newColor = Color.yellow; //color change

    private Renderer objectRenderer; //


 private void Awake()


    {
           // Show the object
        objectToShow.SetActive(true);
       
        Recenter();
       
    }

    void Start()
    {
        // Change the color of the objectToShow
        if (objectRenderer != null)
        {
            objectRenderer.material.color = newColor;
        }
    }

  public void Recenter()
{
    Vector3 offset = head.position - origin.position;
    offset.y = 0;
    origin.position = target.position;
}
}
