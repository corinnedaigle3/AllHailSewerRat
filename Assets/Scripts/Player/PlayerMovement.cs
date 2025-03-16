using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float groundDrag;

    public float jumpForce;
    public float jmpCooldown;
    public float airMulti;
    public bool readyToJump = true;


    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask ground;
    bool isGround;

    public Transform orientation;

    float hInput;
    float vInput;

    Vector3 moveDirection;
    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
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
        moveDirection = orientation.forward * vInput + orientation.right * hInput;
        if (isGround)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        } else if (!isGround)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMulti, ForceMode.Force);

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
