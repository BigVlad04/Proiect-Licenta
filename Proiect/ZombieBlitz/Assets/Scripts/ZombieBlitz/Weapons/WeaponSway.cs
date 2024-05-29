using UnityEngine;
/// <summary>
/// this script makes weapons sway realistically when the player moves the mouse
/// </summary>
public class WeaponSway : MonoBehaviour
{
    public float swayAmount = 0.02f;
    public float maxSwayAmount = 0.06f;
    public float smoothAmount = 6f;
    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.localPosition;
    }

    void Update()
    {
        float moveX = -Input.GetAxis("Mouse X") * swayAmount;
        float moveY = -Input.GetAxis("Mouse Y") * swayAmount;
        moveX = Mathf.Clamp(moveX, -maxSwayAmount, maxSwayAmount);
        moveY = Mathf.Clamp(moveY, -maxSwayAmount, maxSwayAmount);

        Vector3 finalPosition = new Vector3(moveX, moveY, 0f);
        transform.localPosition = Vector3.Lerp(transform.localPosition, finalPosition + initialPosition, Time.deltaTime * smoothAmount);
    }
}