using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    private int currentScene = 0;
    
    public void OnClick()
    {
        SceneManager.LoadScene(currentScene + 1);
    }

    public void Onquit()
    {
        Application.Quit();
    }
    void Start()
    {
        
        
    }

    
    void Update()
    {
        
    }
}
