using UnityEngine;
using UnityEngine.AI;
using TMPro;
using UnityEngine.SceneManagement;

public class RatKing : MonoBehaviour
{
    [Header("Nav Mesh")]
    public NavMeshAgent agent; // NavMeshAgent component for navigation
    public Transform player; // Reference to the player
    public GameObject playerO; // Reference to the player GameObject
    public GameObject ratKing; // Reference to the Rat King object
    public LayerMask whatIsGround, whatIsPlayer; // Layers for ground and player detection

    [Header("Music")]
    public AudioSource kingAttackSound; // Sound played during attack
    public AudioSource chaseMusic; // Music played during chase
    public AudioSource bM1; // Background music 1
    public AudioSource bM2; // Background music 2

    [Header("Attacking")]
    public float timeBetweenAttacks; // Delay between attacks
    private bool alreadyAttacked; // Tracks if attack cooldown is active
    public GameObject projectileKey; // Projectile fired without cheese
    public GameObject projectileCheese; // Projectile fired with cheese
    public GameObject spawnPoint; // Position from which projectiles are spawned

    [Header("States")]
    public float sightRange, attackRange, textRange; // Ranges for sight, attack, and text
    public bool playerInSightRange, playerInAttackRange, playerInTextRange; // Detection booleans

    [Header("Dodge Player")]
    public float detectionPauseTime; // Time to pause after detection (unused here)
    public float distanceFromPlayer = 5f; // Distance to keep from player when dodging

    [Header("Dodge Projectiles")]
    public float dodgeDistance = 2f; // Distance to dodge
    public float dodgeSpeed = 5f; // Speed of dodge movement
    public float dodgeAngle = 90f; // Angle of dodge (not fully used)
    private Rigidbody rb; // Rigidbody for physics movement
    private bool isDodging = false; // Is currently dodging

    [Header("Script")]
    public TextMeshProUGUI ratKingTextC1; // Cutscene text line 1
    public TextMeshProUGUI ratKingTextC2; // Cutscene text line 2
    public TextMeshProUGUI ratKingTextC3; // Cutscene text line 3
    public TextMeshProUGUI ratKingTextC4; // Cutscene text line 4
    public TextMeshProUGUI ratKingTextK1; // Key text line 1
    public TextMeshProUGUI ratKingTextK2; // Key text line 2
    public TextMeshProUGUI presentTextP1; // Placeholder for player text (not used)
    private float textTimeC = 12f; // Timer for cutscene text
    private float textTimeK = 6f; // Timer for key text

    [Header("Bools")]
    public bool ratKingDead = false; // Is the Rat King dead
    public bool isTalking = false; // Is currently talking
    public bool DoorWithCheese = false; // Does player have the cheese
    public bool talkingDone = false; // Finished talking
    GotItem cheese; // Reference to player's item script

    public Animator animator; // Animator for Rat King animations

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform; // Get player transform
        playerO = GameObject.Find("Player"); // Get player GameObject
        agent = GetComponent<NavMeshAgent>(); // Get NavMeshAgent
        transform.LookAt(player.transform); // Face the player at start
        rb = GetComponent<Rigidbody>(); // Get Rigidbody
        animator = ratKing.GetComponentInChildren<Animator>(); // Get Rat King Animator
        animator.SetBool("isRunnngF", false); // Set initial animation states
        animator.SetBool("isRunningB", false);

        bM1.Play(); // Start background music
        bM2.Play();
    }

    private void Update()
    {
        transform.LookAt(player.transform); // Keep facing the player
        cheese = playerO.GetComponent<GotItem>(); // Get player's item script
        DoorWithCheese = cheese.OpenDoorWithCheese; // Check if player has cheese

        // Check for ranges
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        playerInTextRange = Physics.CheckSphere(transform.position, textRange, whatIsPlayer);

        // Decide action based on player ranges
        if (playerInSightRange && playerInTextRange && !playerInAttackRange)
        {
            ChasePlayer(); // Chase the player
        }
        if (playerInSightRange && playerInTextRange && playerInAttackRange)
        {
            AttackPlayer(); // Attack the player
        }
        if (!playerInSightRange && !playerInAttackRange && playerInTextRange)
        {
            TextUpdate(); // Update dialogue text
        }
    }

    private void ChasePlayer()
    {
        if (DoorWithCheese == false && ratKingDead == false)
        {
            agent.SetDestination(player.transform.position); // Move toward player
            animator.SetBool("isRunnngF", true); // Forward animation
            animator.SetBool("isRunningB", false);
        }
        if (DoorWithCheese == true && ratKingDead == false)
        {
            Vector3 moveFromPlayer = transform.position - player.position; // Move away
            agent.SetDestination(transform.position + moveFromPlayer.normalized * distanceFromPlayer);
            Dodge(); // Attempt dodge
            animator.SetBool("isRunnngF", false);
            animator.SetBool("isRunningB", true); // Backward animation
        }
    }

    // Called when player is in attack range
    void AttackPlayer()
    {
        agent.SetDestination(transform.position); // Stop moving

        if (!alreadyAttacked)
        {
            // Fire projectile
            if (DoorWithCheese == true)
            {
                Rigidbody rb = Instantiate(projectileCheese, spawnPoint.transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            }
            if (DoorWithCheese == false)
            {
                Rigidbody rb = Instantiate(projectileKey, spawnPoint.transform.position, Quaternion.identity).GetComponent<Rigidbody>();
                Debug.Log("Die!");
            }

            rb.AddForce(transform.forward * 60f, ForceMode.Impulse); // Launch forward
            rb.AddForce(transform.up * 4f, ForceMode.Impulse); // Launch upward

            if (DoorWithCheese == true && ratKingDead == false)
            {
                alreadyAttacked = true; // Start attack cooldown
                Invoke(nameof(ResetAttack), timeBetweenAttacks);
            }
            else
            {
                alreadyAttacked = false; // Can attack immediately
            }
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false; // Reset attack cooldown
    }

    private void Dodge()
    {
        if (isDodging)
        {
            rb.MovePosition(rb.position + transform.up * dodgeSpeed * Time.deltaTime); // Move upward slightly
            isDodging = false; // Reset dodge state
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ProjectileDodgeWindow")
        {
            isDodging = true; // Enable dodge
            Vector3 dodgeDirection = Vector3.right; // Dodge right
            rb.AddForce(dodgeDirection * dodgeSpeed, ForceMode.Impulse); // Apply dodge force
        }

        if (other.tag == "Projectile")
        {
            SceneManager.LoadScene("Lose"); // Player hit ? lose
        }
    }

    private void TextUpdate()
    {
        // Countdown timers for dialogue
        textTimeC -= Time.deltaTime;
        textTimeK -= Time.deltaTime;

        // Cutscene dialogue when player has cheese
        if (DoorWithCheese == true && ratKingDead == false)
        {
            isTalking = true;
            Debug.Log("I am the boss And I am talking " + isTalking);
            if (textTimeC >= 9f)
            {
                ratKingTextC1.gameObject.SetActive(true);
            }
            else if (textTimeC >= 6f)
            {
                ratKingTextC1.gameObject.SetActive(false);
                ratKingTextC2.gameObject.SetActive(true);
            }
            else if (textTimeC >= 3f)
            {
                ratKingTextC2.gameObject.SetActive(false);
                ratKingTextC3.gameObject.SetActive(true);
            }
            else if (textTimeC >= 1f)
            {
                ratKingTextC3.gameObject.SetActive(false);
                ratKingTextC4.gameObject.SetActive(true);
                bM1.Stop(); // Stop background music
                bM2.Stop();
                chaseMusic.Play(); // Start chase music
            }
            else
            {
                talkingDone = true;
                sightRange = textRange;
                ratKingTextC1.gameObject.SetActive(false);
                ratKingTextC2.gameObject.SetActive(false);
                ratKingTextC3.gameObject.SetActive(false);
                ratKingTextC4.gameObject.SetActive(false);
                isTalking = false; // Done talking
            }
        }

        // Dialogue when player does not have cheese
        if (DoorWithCheese == false && ratKingDead == false)
        {
            isTalking = true;
            Debug.Log("I am the boss And I am talking " + isTalking);

            if (textTimeK >= 3f)
            {
                ratKingTextK1.gameObject.SetActive(true);
            }
            else if (textTimeK >= 2f)
            {
                kingAttackSound.Play(); // Play attack sound
            }
            else if (textTimeK >= 1f)
            {
                ratKingTextK1.gameObject.SetActive(false);
                ratKingTextK2.gameObject.SetActive(true);
            }
            else
            {
                talkingDone = true;
                sightRange = textRange;
                ratKingTextK1.gameObject.SetActive(false);
                ratKingTextK2.gameObject.SetActive(false);
                isTalking = false; // Done talking
            }
        }
    }
}
