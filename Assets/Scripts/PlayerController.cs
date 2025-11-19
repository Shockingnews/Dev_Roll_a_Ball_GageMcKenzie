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
    private float DashCurrentTime = 1;
    private bool DashTimerActive;
    public float speed = 0;
    public float speedBoost = 0;
    public float jumpForce;
    static bool Shield = false;
    static bool speedPowerUp = false;
    static bool JumpPowerUp = false;
    static bool dashPowerUpCanDash = true;
    static bool dashPowerUpCanHit = false;
    static int test;

    
    public GameObject ChaserNum1; 
    public GameObject ChaserNum2;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public GameObject restartButton;
    public GameObject continueButton;
    static GameObject enemy;

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
        TimeSpan dashTime = TimeSpan.FromSeconds(DashCurrentTime);
        if (DashTimerActive)
        {
            DashCurrentTime = DashCurrentTime - Time.deltaTime;
            if(DashCurrentTime <= 0)
            {
                dashPowerUpCanHit = false;
                DashTimerActive = false;
                DashCurrentTime = 1;
            }
        }
        if (dashPowerUpCanDash == true) 
        {
            

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Vector3 dashmovement = new Vector3(movementX, 0.0f, movementY);
                rb.AddForce(dashmovement * 999);
                dashPowerUpCanHit = true;
                dashPowerUpCanDash = false;
                DashTimerActive = true;
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
            dashPowerUpCanDash = false;
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
            dashPowerUpCanDash = true;
            test = 1;
            count = count + 1;
            other.gameObject.SetActive(false);
            
            SetCountText();


        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("JumpPad"))
        {
            Vector3 jumpmovement = new Vector3(0.0f, 0.5f, 0.0f);
            rb.AddForce(jumpmovement * jumpForce);

        }
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= countMax)
        {
            
            winTextObject.SetActive(true);
            continueButton.gameObject.SetActive(true);
            GameObject enemy = GameObject.FindWithTag("Enemy");
            Destroy(enemy);


        }
    }

     void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            

            if (Shield == false)
            {
                if(dashPowerUpCanHit == true)
                {
                    collision.gameObject.SetActive(false);
                    
                }
                
                if (dashPowerUpCanHit == false)
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
                    if (dashPowerUpCanHit == true)
                    {
                        collision.gameObject.SetActive(false);
                        
                    }
                    
                    if (dashPowerUpCanHit == false)
                    {
                        Destroy(gameObject);
                        winTextObject.gameObject.SetActive(true);
                        restartButton.gameObject.SetActive(true);
                        winTextObject.GetComponent<TextMeshProUGUI>().text = "You Lose!";
                    }
                    test = 1;
                    

                }
            }
            
        }
    }
    
}
