using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    static int Menu = 0;
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
    public void OnMenu()
    {
        
        SceneManager.LoadScene(Menu);

    }
    public void OnQuit()
    {
        Application.Quit();

    }
}
