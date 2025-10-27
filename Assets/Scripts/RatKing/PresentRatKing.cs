using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PresentRatKing : MonoBehaviour
{
    public Transform player; // Reference to the player
    public LayerMask whatIsGround, whatIsPlayer; // Layers to detect player and ground

    public float sightRange; // Radius within which the player can be detected
    public bool playerInSightRange; // True if the player is within sight

    public TextMeshProUGUI presentTextP1; // UI text displayed when player is in range
    private float textTimeP = 3f; // Timer for showing text before scene transition

    // Start is called before the first frame update
    void Awake()
    {
        transform.LookAt(player); // Rotate to face the player at start
    }

    // Update is called once per frame
    void Update()
    {
        // Check if player is within sight range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);

        if (playerInSightRange)
        {
            textTimeP -= Time.deltaTime; // Countdown timer

            if (textTimeP >= 1f)
            {
                presentTextP1.gameObject.SetActive(true); // Show UI text
            }
            else
            {
                presentTextP1.gameObject.SetActive(false); // Hide UI text
                SceneManager.LoadScene("WinScreen"); // Load the win screen scene
            }
        }
    }
}