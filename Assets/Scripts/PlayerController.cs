using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    
    private Rigidbody rb;
    public int countMax;
    private int count;
    private float movementX;
    private float movementY;
    public float timelasting;
    private bool timerActive;
    private float currentTime;
    private float shieldCurrentTime;
    private bool shieldTimerActive;
    public float speed = 0;
    public float speedBoost = 0;
    public float jumpForce;
    static bool Shield = false;
    static bool speedPowerUp = false;
    static bool JumpPowerUp = false;
    static string dashPowerUp = "can't Hit";
    static int test;

    
    public GameObject ChaserNum1; 
    public GameObject ChaserNum2;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public GameObject restartButton;
    public GameObject continueButton;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentTime = timelasting;
        shieldCurrentTime = 3;

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
     void Update()
    {
        if (JumpPowerUp == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Vector3 jumpmovement = new Vector3(0.0f, 0.5f, 0.0f);
                rb.AddForce(jumpmovement * jumpForce);
                JumpPowerUp = false;
            }
        }
        if (speedPowerUp == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                currentTime = timelasting;
                timerActive = true;
                speedPowerUp = false;
            }
        }
        if (dashPowerUp == "can dash") 
        {
            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Vector3 dashmovement = new Vector3(movementX, 0.0f, movementY);
                rb.AddForce(dashmovement * 999);
                dashPowerUp = "can hit";
                test = 2;
                
                


                
            }
            

        }
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
                currentTime = timelasting;

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
                shieldCurrentTime = 3;
                
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
            currentTime = timelasting;
            timerActive = true;

        }
        if (other.gameObject.CompareTag("Spawner"))
        {
            ChaserNum1.gameObject.SetActive(true);
            ChaserNum2.gameObject.SetActive(false);

        }
        if (other.gameObject.CompareTag("DeSpawner"))
        {
            ChaserNum1.gameObject.SetActive(false);
            ChaserNum2.gameObject.SetActive(true);

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
        if (other.gameObject.CompareTag("speedPowerUp"))
        {
            speedPowerUp = true;

            count = count + 1;
            other.gameObject.SetActive(false);
            SetCountText();
        }
        if (other.gameObject.CompareTag("jumpPowerUp"))
        {
            
            
            count = count + 1;
            other.gameObject.SetActive(false);
            JumpPowerUp = true;
            SetCountText();
            
            
        }
        if (other.gameObject.CompareTag("dashPowerUp"))
        {

            test = 1;
            count = count + 1;
            other.gameObject.SetActive(false);
            dashPowerUp = "can dash";
            SetCountText();


        }
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= countMax)
        {
            
            winTextObject.SetActive(true);
            continueButton.gameObject.SetActive(true);
            Destroy(GameObject.FindGameObjectWithTag("Enemy"));
            

        }
    }

     void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            
            if (Shield == false)
            {
                if(dashPowerUp == "can hit")
                {
                    Destroy(GameObject.FindGameObjectWithTag("Enemy"));
                }
                if(dashPowerUp != "can hit")
                {
                    Destroy(gameObject);
                    winTextObject.gameObject.SetActive(true);
                    restartButton.gameObject.SetActive(true);
                    winTextObject.GetComponent<TextMeshProUGUI>().text = "You Lose!";
                }
                
            }
        }
        if (test != 1)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {

                if (Shield == false)
                {
                    if (dashPowerUp == "can hit")
                    {
                        Destroy(GameObject.FindGameObjectWithTag("Enemy"));
                    }
                    
                    if (dashPowerUp != "can hit")
                    {
                        Destroy(gameObject);
                        winTextObject.gameObject.SetActive(true);
                        restartButton.gameObject.SetActive(true);
                        winTextObject.GetComponent<TextMeshProUGUI>().text = "You Lose!";
                    }
                    test = 1;
                    dashPowerUp = "can't hit";

                }
            }
            
        }
    }
    
}
