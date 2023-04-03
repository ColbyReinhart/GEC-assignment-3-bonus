using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Scripting for player movement and camera control
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 3;
    public float jumpSpeed = 7;
    public float lookSensitivity = 1;

    private const float gravityValue = -9.81f;
    private float playerYVelocity = 0;
    private float mouseX = 0;
    private float mouseY = 0;
    private Vector3 externalForce = Vector3.zero;

    private CharacterController controller;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        // Let the player look around when they move the mouse
        mouseX += Input.GetAxis("Mouse X") * lookSensitivity;
        mouseY += Input.GetAxis("Mouse Y") * lookSensitivity;

        // Clamp x rotation to prevent the user from turning the camera upside down
        mouseY = Mathf.Clamp(mouseY, -90f, 90f);

        // Do the rotations. Y rotations will be applied to the parent gameobject also
        // so that looking left and right also steers the player
        Camera.main.transform.rotation = Quaternion.Euler(-mouseY, mouseX, 0);
        transform.rotation = Quaternion.Euler(0, mouseX, 0);

        // Calculate movement
        Vector3 forwardMovement = transform.forward * Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        Vector3 sideMovement = transform.right * Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;

        // Handle jumping and gravity
        if (Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            playerYVelocity = jumpSpeed;
        }
        else if (!controller.isGrounded)
        {
            playerYVelocity += gravityValue * Time.deltaTime;
        }
        Vector3 vertMovement = Vector3.up * playerYVelocity * Time.deltaTime;

        // Apply all movement
        controller.Move(forwardMovement + sideMovement + vertMovement + externalForce);
        if (externalForce != Vector3.zero)
        {
            externalForce = Vector3.zero;
        }
    }
}
