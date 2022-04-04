using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Include the namespace required to use Unity UI and Input System
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    // Create public variables for player speed, and for the Text UI game objects
    public float speed = 0;
    //public float jumpSpeed = 5f;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public float DisttoGround = 1f;
    
    private Rigidbody rb;
    private int count;
    private int jumps = 0;
    private float movementX;
    private float movementY;
    private bool isGrounded = false;
    
    // Start is called before the first frame update
    // At the start of the game..
    void Start()
    {
        // Assign the Rigidbody component to our private rb variable
        rb = GetComponent<Rigidbody>();
        // Set the count to zero
        count = 0;
        
        SetCountText();
        // Set the text property of the Win Text UI to an empty string, making the 'You Win' (game over message) blank
        winTextObject.SetActive(false);
    }
    
    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        
        movementX = movementVector.x;
        movementY = movementVector.y;
    }
    
    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 12)
        {
            // Set the text value of your 'winText'
            winTextObject.SetActive(true);
        }
    }
    
    void FixedUpdate()
    {
        // Create a Vector3 variable, and assign X and Z to feature the horizontal and vertical float variables above
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        
        rb.AddForce(movement * speed);
        GroundCheck();
    }
    
    void OnJump()
    {
        if(isGrounded)
        {
            jumps += 1;
            Vector3 jump = new Vector3(0.0f, 40f, 0.0f);
            rb.AddForce(jump * speed);
        } else if(jumps < 1) {
            jumps += 1;
            Vector3 doublejump = new Vector3(0.0f, 40f, 0.0f);
            rb.AddForce(doublejump * speed);
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        // ..and if the GameObject you intersect has the tag 'Pick Up' assigned to it..
        if(other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            // ..and if the GameObject you intersect has the tag 'Pick Up' assigned to it..
            count = count + 1;
            
            // Run the 'SetCountText()' function (see below)
            SetCountText();
        }
    }
        
    void GroundCheck()
    {
        if(Physics.Raycast(transform.position, Vector3.down, DisttoGround + 0.1f))
        {
            isGrounded = true;
            jumps = 0;
        } else {
            isGrounded = false;
        }
    }
}
