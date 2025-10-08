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
    private float shieldCurrentTime;
    private bool shieldTimerActive;
    public float speed = 0;
    public float speedBoost = 0;
    public float jumpForce;
    public bool Shield = false;

    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public GameObject restartButton;
    public GameObject continueButton;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentTime = 5;
        shieldCurrentTime = 1;

        continueButton.SetActive(false);
        restartButton.SetActive(false);
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
        TimeSpan shieldTime = TimeSpan.FromSeconds(shieldCurrentTime);
        if (shieldTimerActive)
        {
            Shield = true;
            shieldCurrentTime = shieldCurrentTime - Time.deltaTime;
            if (shieldCurrentTime <= 0)
            {
                Shield = false;
                shieldTimerActive = false;
                shieldCurrentTime = 1;
                
            }

        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("JumpPad"))
        {
            Vector3 jumpmovement = new Vector3(0.0f, 0.5f, 0.0f);
            rb.AddForce(jumpmovement * jumpForce );

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
        if (other.gameObject.CompareTag("PickUpPowerUp"))
        {
            shieldTimerActive = true;
            
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
            continueButton.gameObject.SetActive(true);
            Destroy(GameObject.FindGameObjectWithTag("Enemy"));
            

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {

            if (Shield == false)
            {

                Destroy(gameObject);
                winTextObject.gameObject.SetActive(true);
                restartButton.gameObject.SetActive(true);
                winTextObject.GetComponent<TextMeshProUGUI>().text = "You Lose!";
            }
        }
    }
    
}
