using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public GameObject secondTeleporter;
    public GameObject player;

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player.transform.position = secondTeleporter.transform.position;
            
        }
    }
}
