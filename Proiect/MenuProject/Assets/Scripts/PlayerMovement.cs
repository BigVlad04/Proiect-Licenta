using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement: MonoBehaviour
{

    public Transform orientation;
    public float movementSpeed;

    public float groundDrag;

    public float playerHeight;
    public LayerMask Ground;
    bool grounded;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    public KeyCode jumpKey = KeyCode.Space;

    bool canJump;

    float horizontalInput;
    float verticalInput;

    Vector3 movementDirection;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        canJump = true;
    }

    // Update is called once per frame
    void Update()
    {
        MyInput();
        speedControl();
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, Ground);

        if (grounded)
        {
            rb.drag = groundDrag;
        }
        else 
        {
            rb.drag = 0;    
        }
        
    }

    private void FixedUpdate()
    {
        movePlayer();
    }

    void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        Debug.Log(canJump);

        if (Input.GetKey(jumpKey) && canJump && grounded) { 
            canJump = false;

            jump();

            Invoke(nameof(resetJump), jumpCooldown);
        }
    }

    void movePlayer() 
    {
        movementDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        if(grounded)
            rb.AddForce(movementDirection.normalized * movementSpeed * 10f, ForceMode.Force);
        if(!grounded)
            rb.AddForce(movementDirection.normalized * movementSpeed * airMultiplier * 10f, ForceMode.Force);

    }

    void speedControl() 
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        if (flatVel.magnitude > movementSpeed)
        { 
            Vector3 maxVelocity = flatVel.normalized * movementSpeed;
            rb.velocity = new Vector3(maxVelocity.x, rb.velocity.y, maxVelocity.z);
        }
    }

    void jump() 
    {
        rb.velocity = new Vector3(rb.velocity.x,0f,rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        Debug.Log("Jumped");
    }

    private void resetJump()
    {
        canJump = true;
    }

}
