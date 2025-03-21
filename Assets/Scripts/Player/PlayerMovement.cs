using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Cinemachine;


public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float groundDrag;
    public float rotationSpeed;

    public float jumpForce;
    public float jmpCooldown;
    public float airMulti;
    public bool readyToJump = true;


    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask ground;
    bool isGround;

   // public Transform orientation;

    [SerializeField] Camera cam;

    float hInput;
    float vInput;

    Vector3 moveDirection;
    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        Cursor.lockState = CursorLockMode.Locked;

    }
    void Update()
    {
        // ground check 
        isGround = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, ground);

        playerInputs();

        // handle drag
        if (isGround)
        {
            
            rb.drag = groundDrag;
        }
        else
            rb.drag = 0;
    }
    void FixedUpdate()
    {
        movePlayer();
    }

    void playerInputs()
    {
        vInput = Input.GetAxis("Vertical");
        hInput = Input.GetAxis("Horizontal");

        if (Input.GetKey(KeyCode.Space) && readyToJump && isGround)
        {
            readyToJump = false;
            Jump();

            Invoke(nameof(resetJump), jmpCooldown);
        }
    }

    void movePlayer()
    {
        // Get the camera's forward and right vectors (ignoring y-axis)
        Vector3 camForward = new Vector3(cam.transform.forward.x, 0f, cam.transform.forward.z).normalized;
        Vector3 camRight = new Vector3(cam.transform.right.x, 0f, cam.transform.right.z).normalized;

        // Calculate movement direction based on camera
        moveDirection = camForward * vInput + camRight * hInput;

       
        // Apply movement force
        if (isGround)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        }
        else
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMulti, ForceMode.Force);
        }
       
        // Rotate player to face movement direction without affecting the movement itself
        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime));
        }
        
    }
    void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
       
    }

    private void resetJump()
    {
        readyToJump = true;   
    }
}
