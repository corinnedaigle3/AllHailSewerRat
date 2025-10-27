using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class PatrolRat : MonoBehaviour
{
    PlayerMovement p;
    public LayerMask whatIsGround, whatIsPlayer;
    //public float health;

    [Header("Patroling")]
    public NavMeshAgent agent; // Reference to NavMeshAgent component for navigation
    public Transform patrolRoute; // Parent transform that holds all waypoint positions
    public Transform player; // Reference to the player transform
    public int t = 0; // Index for tracking current waypoint
    private Transform[] locations; // Array to store waypoint transforms

    [Header("States")]
    public int sightRange; // Radius within which the enemy can detect the player
    public bool playerInSightRange; // Boolean to check if player is within sight range

    Vector3 velocity = Vector3.zero; // Used for smooth movement when chasing the player

    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.Find("Player").transform; // Finds the player object in the scene
        agent = GetComponent<NavMeshAgent>(); // Gets the NavMeshAgent attached to this object
        InitializePatrolRoute(); // Sets up waypoints
        MoveToNextPatrolLocation(); // Starts patrolling
    }

    private void FixedUpdate()
    {
        // Check if the player is within sight range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);

        // If the player is not in sight and the agent reached its current destination
        if (!playerInSightRange && agent.remainingDistance < 0.2f)
        {
            MoveToNextPatrolLocation(); // Move to the next waypoint
        }

        // If the player is detected, start chasing
        if (playerInSightRange)
        {
            ChasePlayer();
        }
    }

    void MoveToNextPatrolLocation() // Enemy moves to next patrol waypoint
    {
        agent.updatePosition = true;

        if (locations.Length == 0) return; // If no patrol points are set, do nothing
        {
            agent.SetDestination(locations[t].position); // Move to the next waypoint
            t = (t + 1) % locations.Length; // Cycle through waypoints continuously
        }
    }

    void InitializePatrolRoute() // Initialize the patrol route waypoints
    {
        locations = new Transform[patrolRoute.childCount]; // Create array sized to child count
        for (int i = 0; i < patrolRoute.childCount; i++)
        {
            locations[i] = patrolRoute.GetChild(i); // Store each child transform
        }
    }

    private void ChasePlayer() // Handles chasing behavior
    {
        agent.updatePosition = false; // Temporarily disable agent position updates
        agent.SetDestination(player.position); // Set destination to player's position
        transform.position = Vector3.SmoothDamp(transform.position, agent.nextPosition, ref velocity, 0.1f); // Smooth movement
    }

    void OnCollisionEnter(Collision collision) // Triggered when enemy collides with something
    {
        if (collision.gameObject.name == "Player") // If collided with player
        {
            p = collision.gameObject.GetComponent<PlayerMovement>(); // Get PlayerMovement script
            p.dead = true; // Mark player as dead
            Debug.Log("PlayerDead " + p.dead);

            StartCoroutine(pLose(1f)); // Start coroutine to load lose screen after delay
            Debug.Log("PlayerCaught!");
        }
    }

    IEnumerator pLose(float waitTime) // Handles lose screen transition
    {
        yield return new WaitForSeconds(waitTime); // Wait for specified time
        SceneManager.LoadScene("LoseScreen"); // Load the LoseScreen scene
    }
}
