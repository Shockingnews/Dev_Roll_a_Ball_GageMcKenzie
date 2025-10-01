using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using Unity.VisualScripting;
using System;

public class PlayerController : MonoBehaviour
{
    
    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;
    private bool timerActive;
    private float currentTime;
    public float speed = 0;
    public float speedBoost = 0;
    public float jumpForce;
    

    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentTime = 5;
        
        
        winTextObject.SetActive(false);
        SetCountText();
        count = 0;
        rb = GetComponent<Rigidbody>();
        
    }
    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;

    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
        
        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        if (timerActive)
        {
            currentTime = currentTime - Time.deltaTime;
            speed = speedBoost;
            if (currentTime <= 0)
            {
                timerActive = false;
                speed = 10;
                currentTime = 5;

            }

        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("JumpPad"))
        {
            rb.AddForce(transform.up * jumpForce);

        }

        if (other.gameObject.CompareTag("SpeedBoost"))
        {
                timerActive = true;
            
        }
        
        if (other.gameObject.CompareTag("PickUp"))
        {
            count = count + 1;
            other.gameObject.SetActive(false);
            SetCountText();
        }
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 10)
        {
            winTextObject.SetActive(true);
            Destroy(GameObject.FindGameObjectWithTag("Enemy"));
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            winTextObject.gameObject.SetActive(true);
            winTextObject.GetComponent<TextMeshProUGUI>().text = "You Lose!";
        }
    }
    
}
