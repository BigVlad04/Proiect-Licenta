using UnityEngine;
/// <summary>
/// This script handles player movement
/// </summary>
public class PlayerMovement: MonoBehaviour
{
    //general
    public CharacterController characterController;
    public Transform PlayerBody;
    public float movementSpeed = 12f;
    public float gravity = -10f;

    //input-related
    float horizontalInput;
    float verticalInput;
    Vector3 movementDirection;
    Vector3 velocity;

    //ground-related
    public Transform groundChecker;     //an empty GameObject placed on the foot of the player. Will cast a small invisible sphere to check if the player is on the ground 
    public float groundCheckRadius;     //radius of invisible sphere
    public LayerMask Ground;
    bool isGrounded;

    //jump-related
    public KeyCode jumpKey = KeyCode.Space;
    public float jumpHeight=3f;
    public float jumpCooldown;
    bool canJump;

    void Start()
    {
        canJump = true;
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundChecker.position,groundCheckRadius,Ground);   //cast an invisible sphere from the player's foot to check if player is on the ground   
        if (isGrounded && velocity.y < 0f) { 
            velocity.y = 0f;
        }
        CheckInput();
        movementDirection = PlayerBody.forward * verticalInput + PlayerBody.right * horizontalInput;
        characterController.Move(movementDirection * movementSpeed * Time.deltaTime);
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
        
    }

    void CheckInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        if (Input.GetKey(jumpKey) && canJump && isGrounded) { 
            canJump = false;
            jump();
            Invoke(nameof(resetJump), jumpCooldown);    //invoke resetJump after jumpCooldown time
        }
    }

    void jump() 
    {
        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }

    private void resetJump()
    {
        canJump = true;
    }
}
