using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class RatKing : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;

    [Header("Attacking")]
    public float timeBetweenAttacks;
    private bool alreadyAttacked;
    public GameObject projectileKey;
    public GameObject projectileCheese;
    public GameObject spawnPoint;

    [Header("States")]
    public float sightRange, attackRange, textRange;
    public bool playerInSightRange, playerInAttackRange, playerInTextRange;

    [Header("Dodge Player")]
    public float detectionPauseTime;
    public float distanceFromPlayer = 5f;

    [Header("Dodge Projectiles")]
    public float dodgeDistance = 2f;
    public float dodgeSpeed = 5f;
    public float dodgeAngle = 90f;
    private Rigidbody rb;
    private bool isDodging = false;

    [Header("Script")]
    public TextMeshProUGUI ratKingTextC1;
    public TextMeshProUGUI ratKingTextC2;
    public TextMeshProUGUI ratKingTextC3;
    public TextMeshProUGUI ratKingTextC4;
    public TextMeshProUGUI ratKingTextK1;
    public TextMeshProUGUI ratKingTextK2;
    public TextMeshProUGUI presentTextP1;
    private float textTimeC = 12f;
    private float textTimeK = 6f;

    [Header ("Bools")]
    public bool OpenDoorWithKey;
    public bool OpenDoorWithCheese;
    public bool ratKingDead;
    public bool isTalking = false;

    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        transform.LookAt(player);
        rb = GetComponent<Rigidbody>();
        ratKingDead = false;
        OpenDoorWithCheese = GameObject.Find("Player").GetComponent<GotItem>().OpenDoorWithCheese;
        OpenDoorWithKey = GameObject.Find("Player").GetComponent<GotItem>().OpenDoorWithKey;
    }

    private void Update()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        playerInTextRange = Physics.CheckSphere(transform.position, textRange, whatIsPlayer);

        if (playerInSightRange && playerInTextRange && !playerInAttackRange)
        {
            ChasePlayer();
        }
        if (playerInSightRange && playerInTextRange && playerInAttackRange)
        {
            AttackPlayer();
        }
        if (!playerInSightRange && !playerInAttackRange && playerInTextRange)
        {
            TextUpdate();
        }
    }

    private void ChasePlayer()
    {
        if (OpenDoorWithCheese == false && ratKingDead == false)
        {
            Vector3 moveFromPlayer = transform.position - player.position;
            agent.SetDestination(transform.position + moveFromPlayer.normalized * distanceFromPlayer);
        }
        else 
        {
            agent.SetDestination(player.transform.position * distanceFromPlayer);
        }
    }

    // Update is called once per frame
    void AttackPlayer()
    {
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            //Attack code
            if (OpenDoorWithCheese == true)
            {
                Rigidbody rb = Instantiate(projectileCheese, spawnPoint.transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            }
            else 
            {
                Rigidbody rb = Instantiate(projectileKey, spawnPoint.transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            }

            rb.AddForce(transform.forward * 34f, ForceMode.Impulse);
            rb.AddForce(transform.up * 4f, ForceMode.Impulse);

            if (OpenDoorWithCheese == true && ratKingDead == false)
            {
                alreadyAttacked = true;
                Invoke(nameof(ResetAttack), timeBetweenAttacks);
            }
            else
            {
                alreadyAttacked = false;
            }
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
            //SceneManager.LoadScene("Present");
        }
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    private void TextUpdate()
    {
        //"You dare challenge the all mighty and powerful Rat King!?!"
        textTimeC -= Time.deltaTime;
        textTimeK -= Time.deltaTime;

        if (OpenDoorWithCheese == true && ratKingDead == false)
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
            }
            else
            {
                ratKingTextC1.gameObject.SetActive(false);
                ratKingTextC2.gameObject.SetActive(false);
                ratKingTextC3.gameObject.SetActive(false);
                ratKingTextC4.gameObject.SetActive(false);
                isTalking = false;
            }
        }

        if (OpenDoorWithCheese == false && ratKingDead == false)
        {
            isTalking = true;
            Debug.Log("I am the boss And I am talking " + isTalking);

            if (textTimeK >= 3f)
            {
                ratKingTextK1.gameObject.SetActive(true);
            }
            else if (textTimeK >= 1f)
            {
                ratKingTextK1.gameObject.SetActive(false);
                ratKingTextK2.gameObject.SetActive(true);
            }
            else
            {
                ratKingTextK1.gameObject.SetActive(false);
                ratKingTextK2.gameObject.SetActive(false);
                isTalking = false;
            }
        }
    }
}
