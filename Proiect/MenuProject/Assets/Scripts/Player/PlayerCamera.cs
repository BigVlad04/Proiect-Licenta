using UnityEngine;
/// <summary>
/// This script handles the camera movement with the mouse
/// </summary>
public class PlayerCamera : MonoBehaviour
{
    public float sensX;     
    public float sensY;
    public Transform playerBody;
    float rotationX;
    float rotationY;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;       
        Cursor.visible = false;
    }

    void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        rotationY += mouseX;
        rotationX -= mouseY;

        rotationX = Mathf.Clamp(rotationX, -90, 90);       //for the X rotation we need to clamp the value to make sure the player is not able to look behind him by looking up or down too much

        transform.rotation = Quaternion.Euler(rotationX, rotationY, 0); //rotate the camera
        playerBody.rotation = Quaternion.Euler(0, rotationY, 0);    // rotate the player's body when looking sideways. We do not rotate the player's body when looking up/down
    }
}
