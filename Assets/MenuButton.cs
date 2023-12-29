using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class MenuButton : MonoBehaviour
{
    [SerializeField] private InputActionReference menuActionReference;
    private string sceneToLoad = "Menu";

    // Start is called before the first frame update
    void Start()
    {
        menuActionReference.action.performed += ReturnToMenu;
    }

    // Update is called once per frame
    void Update()
    {
        // Your update logic here
    }

    private void ReturnToMenu(InputAction.CallbackContext obj)
    {
        // Load the specified scene
        SceneManager.LoadScene(sceneToLoad);
    }
}
