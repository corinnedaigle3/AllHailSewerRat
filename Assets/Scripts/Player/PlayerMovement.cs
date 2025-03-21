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
    public bool bTalking;

    // public Transform orientation;

    private GameObject camera;
    Transform cam;
    private GameObject theLookAtPoint;
    Transform lookAt;
    private Transform LookAtPoint;

     float hInput;
     float vInput;

    Vector3 moveDirection;
    Rigidbody rb;

    Vector3 camForward;
    Vector3 camRight;
    Vector3 playerLook;

    // Enemy Reference
    GameObject ratKing;
    RatKing rat;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        Cursor.lockState = CursorLockMode.Locked;
        theLookAtPoint = GameObject.Find("LookAtPoint");
        camera = GameObject.Find("MainCamera");
         cam = theLookAtPoint.GetComponent<Transform>();
        lookAt = theLookAtPoint.GetComponent<Transform>();
        ratKing = GameObject.Find("RatKing");
    }
    void Update()
    {

        PlayerLookRotation();

        // ground check 
        isGround = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, ground);

        // Get the camera's forward and right vectors (ignoring y-axis)
        camForward = new Vector3(cam.transform.forward.x, 0f, cam.transform.forward.z).normalized;
        camRight = new Vector3(cam.transform.right.x, 0f, cam.transform.right.z).normalized;

        // Calculate movement direction based on camera
        moveDirection = camForward * vInput + camRight * hInput;

        playerInputs();

        // unlock shooting      
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
            rb.drag = 0f;

    }
    void FixedUpdate()
    {
        rat = ratKing.GetComponent<RatKing>();

        bTalking = rat.isTalking;
        Debug.Log("Boss is talking " + rat.isTalking);

        Debug.Log("Boss is talking "+ bTalking);
        if (!bTalking)
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

            Invoke(nameof(resetJump), jmpCooldown);
        }
    }

    void movePlayer()
    {

        if (hInput != 0 || vInput != 0)
        {
            rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime);
            
        }

    }
    void Jump()
    {
        Debug.Log("Current rb velocity " + rb.velocity.magnitude);
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        Debug.Log("Current rb velocity " + rb.velocity.magnitude);

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
