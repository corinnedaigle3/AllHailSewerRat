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

    [Header("Bullet")]
    public GameObject bullet;
   public  bool shooting, readyToShoot;

    // public Transform orientation;

    public Camera cam;
    public Transform LookAtPoint;

     float hInput;
     float vInput;

    Vector3 moveDirection;
    Rigidbody rb;

    Vector3 camForward;
    Vector3 camRight;
    Vector3 playerLook;
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

        // Get the camera's forward and right vectors (ignoring y-axis)

        camForward = new Vector3(cam.transform.forward.x, 0f, cam.transform.forward.z).normalized;
        camRight = new Vector3(cam.transform.right.x, 0f, cam.transform.right.z).normalized;

        moveDirection = camForward * vInput + camRight * hInput;

        // Rotate player to face movement direction without affecting the movement itself
        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime));
        }

        playerInputs();


        if (shooting && readyToShoot)
        {
            PlayerShoot();
        }

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

        shooting = Input.GetKeyDown(KeyCode.Mouse0);

        if (Input.GetKey(KeyCode.Space) && readyToJump && isGround)
        {
            readyToJump = false;
            Jump();

            Invoke(nameof(resetJump), jmpCooldown);
        }
    }

    void movePlayer()
    {
        // Calculate movement direction based on camera
        

        // Apply movement force
        if (isGround)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        }
        else
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
    void PlayerShoot()
    {
       
            Instantiate(bullet, LookAtPoint.position, Quaternion.identity);
           // bullet.GetComponent<Rigidbody>().AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        
    }
}
