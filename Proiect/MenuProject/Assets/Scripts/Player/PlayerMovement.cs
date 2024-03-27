using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement: MonoBehaviour
{

    public CharacterController characterController;

    public Transform PlayerBody;
    public float movementSpeed = 12f;
    public float gravity = -10f;
    float horizontalInput;
    float verticalInput;
    Vector3 movementDirection;
    Vector3 velocity;
    public Transform groundChecker;
    public float groundCheckRadius;
    public LayerMask Ground;
    bool isGrounded;
    public KeyCode jumpKey = KeyCode.Space;
    public float jumpHeight=3f;
    public float jumpCooldown;
    bool canJump;

    /*public float groundDrag;
    public float playerHeight;
    
    public float airMultiplier;
    

   */


    //Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        canJump = true;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundChecker.position,groundCheckRadius,Ground);
        if (isGrounded && velocity.y < 0f) { 
            velocity.y = 0f;
        }
        MyInput();
        movementDirection = PlayerBody.forward * verticalInput + PlayerBody.right * horizontalInput;
        characterController.Move(movementDirection * movementSpeed * Time.deltaTime);
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
        
    }

    void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        if (Input.GetKey(jumpKey) && canJump && isGrounded) { 
            canJump = false;
            jump();
            Invoke(nameof(resetJump), jumpCooldown);
        }
    }

   /* void movePlayer() 
    {
        movementDirection = PlayerBody.forward * verticalInput + PlayerBody.right * horizontalInput;
        if(grounded)
            rb.AddForce(movementDirection.normalized * movementSpeed * 10f, ForceMode.Force);
        if(!grounded)
            rb.AddForce(movementDirection.normalized * movementSpeed * airMultiplier * 10f, ForceMode.Force);

    }*/

  /*  void speedControl() 
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        if (flatVel.magnitude > movementSpeed)
        { 
            Vector3 maxVelocity = flatVel.normalized * movementSpeed;
            rb.velocity = new Vector3(maxVelocity.x, rb.velocity.y, maxVelocity.z);
        }
    }*/

    void jump() 
    {
        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }

    private void resetJump()
    {
        canJump = true;
    }

}
