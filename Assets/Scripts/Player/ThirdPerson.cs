using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPerson : MonoBehaviour
{
    [Header("References")]
    public Transform orientation;
    public Transform player;
    public Transform playerObj;
    public Rigidbody rb;

    public float rotationSpeed;

    public Transform combatLookAtP;

    private float horizontalInput;
    private float verticalInput;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {

        // Get input for movement
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        // If there is input (movement keys), rotate the player
        if (horizontalInput != 0 || verticalInput != 0)
        {
            // Calculate direction relative to camera orientation
            Vector3 inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

            // If movement input direction is non-zero, rotate player
            if (inputDir != Vector3.zero)
            {
                // Rotate the player object smoothly
                playerObj.forward = Vector3.Slerp(playerObj.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
            }

            // Optional: Add force for movement
            Vector3 moveDir = inputDir.normalized * Time.deltaTime * 5f; // Adjust speed here
            rb.MovePosition(rb.position + moveDir);
        }

        // Rotate the camera to face the player
        Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        orientation.forward = viewDir.normalized;

        // Combat look-at logic
        if (combatLookAtP != null)
        {
            Vector3 dirToCombatLookAt = combatLookAtP.position - new Vector3(transform.position.x, combatLookAtP.position.y, transform.position.z);
            orientation.forward = dirToCombatLookAt.normalized;
            playerObj.forward = dirToCombatLookAt.normalized;
        }
    }
}
