using UnityEngine;


public class controlstextscript : MonoBehaviour
{
    public float timelasting;
    private bool timerActive;
    private float currentTime;
    public GameObject controllerText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentTime = timelasting;
        timerActive = true;
        controllerText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (timerActive)
        {
            controllerText.SetActive(true);
            currentTime = currentTime - Time.deltaTime;
            
        }

        if(currentTime <= 0)
        {
            timerActive = false;
            controllerText.SetActive(false);
            currentTime = timelasting;
        }
        
    }
}
