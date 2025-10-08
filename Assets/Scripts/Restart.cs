using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
     
    public void OnRestart()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
  
    }
    public void OnContinue()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene+1);

    }
}
