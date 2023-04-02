using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Scripting for player movement and camera control
public class PlayerMovement : MonoBehaviour
{
    public GameObject mainCamera;
    public float moveSpeed = 3;
    public float jumpForce = 1;
    public float lookSensitivity = 1;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        // Let the player look around when they move the mouse
        float mouseX = Input.GetAxis("Mouse X") * lookSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * lookSensitivity;
        mainCamera.transform.Rotate(-mouseY, 0, 0, Space.Self);
        transform.Rotate(0, mouseX, 0, Space.World);
    }

    private void FixedUpdate()
    {
        // Handle walking
        if (Input.GetKey(KeyCode.UpArrow))
        {
            //Move the Rigidbody forwards constantly at speed you define (the blue arrow axis in Scene view)
            rb.velocity = transform.forward * moveSpeed;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            //Move the Rigidbody backwards constantly at the speed you define (the blue arrow axis in Scene view)
            rb.velocity = -transform.forward * moveSpeed;
        }

        // Handle jumping
        if (Input.GetButtonDown("Jump"))
        {
            rb.AddForce(new Vector3(0, jumpForce, 0));
        }
    }
}
