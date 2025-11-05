using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public GameObject player;
    public GameObject winText;
    public GameObject continue_button;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(player);
        winText.SetActive(true);
        continue_button.SetActive(true);
    }
}
