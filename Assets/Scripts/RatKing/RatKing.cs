using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RatKing : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;
    public float health = 10f;
    public float damage = 2f;

    [Header("Attacking")]
    public float timeBetweenAttacks;
    private bool alreadyAttacked;
    public GameObject projectile;

    [Header("States")]
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    [Header("Dodge Player")]
    public float detectionPauseTime;
    public float distanceFromPlayer = 5f;

    [Header("Dodge Projectiles")]
    public float dodgeDistance = 2f;
    public float dodgeSpeed = 5f;
    public float dodgeAngle = 90f;

    private Rigidbody rb;
    private bool isDodging = false;

    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        transform.LookAt(player);
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInSightRange && playerInAttackRange) AttackPlayer();
    }

    private void ChasePlayer()
    {
        //make sure enemy doesn't move
        Vector3 moveFromPlayer = transform.position - player.position;
        agent.SetDestination(transform.position + moveFromPlayer.normalized * distanceFromPlayer);
    }

    // Update is called once per frame
    void AttackPlayer()
    {
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            //Attack code
            Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();

            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb.AddForce(transform.up * 4f, ForceMode.Impulse);

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void Dodge()
    {
        if (isDodging)
        {
            // Dodge movement
            rb.MovePosition(rb.position + transform.up * dodgeSpeed * Time.deltaTime);
            isDodging = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ProjectileDodgeWindow")
        {
            // Dodge
            isDodging = true;
            Vector2 dodgeDirection = Vector2.right;
            rb.AddForce(dodgeDirection * dodgeSpeed, ForceMode.Impulse);
        }

        if (other.tag == "Projectile")
        {
            health -= damage;

            if (health <= 0) Invoke(nameof(DestroyEnemy), 0.5f);
        }
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}
