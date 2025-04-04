using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float groundDrag;
    public bool dead = false;

    public float jumpForce;
    public float jmpCooldown;
    public float airMulti;
    public bool readyToJump = true;
    public bool isJumping;


    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask ground;
    public bool isGround;
    public bool isMoving;

    [Header("Bullet")]
    public GameObject bullet;
    public  bool shooting, readyToShoot;
    public bool bTalking;

    // public Transform orientation;

    private GameObject playerCamera;
    Transform cam;
    private GameObject theLookAtPoint;
    Transform lookAt;
    private Transform LookAtPoint;

     public float hInput;
     public float vInput;

    public Vector3 moveDirection;
    Rigidbody rb;

    Vector3 camForward;
    Vector3 camRight;
    Vector3 playerLook;

    // Enemy Reference
    public GameObject ratKing;
    RatKing rat;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        Cursor.lockState = CursorLockMode.Locked;
        theLookAtPoint = GameObject.Find("LookAtPoint");
        playerCamera = GameObject.Find("MainCamera");
         cam = theLookAtPoint.GetComponent<Transform>();
        lookAt = theLookAtPoint.GetComponent<Transform>();
        ratKing = GameObject.Find("RatKing");
    }
    void Update()
    {
        PlayerLookRotation();


        Vector3 rayOrigin = transform.position + Vector3.up * 0.1f; // Small offset to avoid ground clipping
        float rayDistance = 0.6f; // Slightly increased distance
        isGround = Physics.Raycast(rayOrigin, Vector3.down, rayDistance, ground);

        // Debug the ray
        Debug.DrawRay(rayOrigin, Vector3.down * rayDistance, isGround ? Color.green : Color.red);
        //isGround = Physics.Raycast(transform.position, Vector3.down, 0.5f, ground);

        // Get the camera's forward and right vectors (ignoring y-axis)
        camForward = new Vector3(cam.transform.forward.x, 0f, cam.transform.forward.z).normalized;
        camRight = new Vector3(cam.transform.right.x, 0f, cam.transform.right.z).normalized;

        // Calculate movement direction based on camera
        moveDirection = camForward * vInput + camRight * hInput;

        playerInputs();

        // unlock shooting      
        if (shooting && readyToShoot && !bTalking)
        {
            PlayerShoot();
        }

        // handle drag
        if (isGround)
        {
            rb.drag = groundDrag;

        }
        else
            rb.drag = 0f;

    }


    void FixedUpdate()
    {
        rat = ratKing.GetComponent<RatKing>();

        bTalking = rat.isTalking;
       // Debug.Log("Boss is talking " + rat.isTalking);

       // Debug.Log("Boss is talking "+ bTalking);
        if (!bTalking && !dead)
        {
            movePlayer();

        }
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
            isJumping = true;


            Invoke(nameof(resetJump), jmpCooldown);
        }
    }

    void movePlayer()
    {
        if (hInput != 0 || vInput != 0)
        {
            isMoving = true;
            rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime);
            
        } else
        {
            isMoving = false;
        }

    }
    void Jump()
    {
        //Debug.Log("Current rb velocity " + rb.velocity.magnitude);
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
       // Debug.Log("Current rb velocity " + rb.velocity.magnitude);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
       
    }

    private void resetJump()
    {
        readyToJump = true;   
    }
    void PlayerShoot()
    {
        LookAtPoint = theLookAtPoint.GetComponent<Transform>();
            Instantiate(bullet, LookAtPoint.position, Quaternion.identity);
           // bullet.GetComponent<Rigidbody>().AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        
    }

    // Rotates the player according to look at point locatin 
    void PlayerLookRotation()
    {
        Vector3 flatForward = new Vector3(lookAt.transform.forward.x, 0f, lookAt.transform.forward.z).normalized;
        if (flatForward != Vector3.zero)
        {
            rb.rotation = Quaternion.LookRotation(flatForward);
        }
    }
}
