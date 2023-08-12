using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
 public string sceneName; // The name of the scene you want to load

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}


   
