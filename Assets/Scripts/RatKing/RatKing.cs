using UnityEngine;
using UnityEngine.AI;
using TMPro;
using System.Runtime.CompilerServices;

public class RatKing : MonoBehaviour
{
    [Header("Nav Mesh")]
    public NavMeshAgent agent;
    public Transform player;
    public GameObject playerO;
    public LayerMask whatIsGround, whatIsPlayer;

    [Header("Music")]
    public AudioSource kingAttackSound;
    public AudioSource chaseMusic;
    public AudioSource bM1;
    public AudioSource bM2;

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
    public bool ratKingDead = false;
    public bool isTalking = false;
    public bool DoorWithCheese = false;
    GotItem cheese; 

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
        playerO = GameObject.Find("Player");
        agent = GetComponent<NavMeshAgent>();
        transform.LookAt(player.transform);
        rb = GetComponent<Rigidbody>();
        
        bM1.Play();
        bM2.Play();
    }

    private void Update()
    {
        cheese = playerO.GetComponent<GotItem>();
        DoorWithCheese = cheese.OpenDoorWithCheese;

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
        if (DoorWithCheese == false && ratKingDead == false)
        {
            agent.SetDestination(player.transform.position);
        }
        if (DoorWithCheese == true && ratKingDead == false)
        {
            Vector3 moveFromPlayer = transform.position - player.position;
            agent.SetDestination(transform.position + moveFromPlayer.normalized * distanceFromPlayer);
        }
    }

    // Update is called once per frame
    void AttackPlayer()
    {
        agent.SetDestination(transform.position);

        

        if (!alreadyAttacked)
        {
            //Attack code
            if (DoorWithCheese == true)
            {
                Rigidbody rb = Instantiate(projectileCheese, spawnPoint.transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            }
            if (DoorWithCheese == false)
            {
                Rigidbody rb = Instantiate(projectileKey, spawnPoint.transform.position, Quaternion.identity).GetComponent<Rigidbody>();
                Debug.Log("Die!");
            }

            rb.AddForce(transform.forward * 60f, ForceMode.Impulse);
            rb.AddForce(transform.up * 4f, ForceMode.Impulse);

            if (DoorWithCheese == true && ratKingDead == false)
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
            Vector3 dodgeDirection = Vector3.right;
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
                bM1.Stop();
                bM2.Stop();
                chaseMusic.Play();
            }
            else
            {
                sightRange = textRange;
                ratKingTextC1.gameObject.SetActive(false);
                ratKingTextC2.gameObject.SetActive(false);
                ratKingTextC3.gameObject.SetActive(false);
                ratKingTextC4.gameObject.SetActive(false);
                isTalking = false;
            }
        }

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
                kingAttackSound.Play();
            }
            else if (textTimeK >= 1f)
            {
                ratKingTextK1.gameObject.SetActive(false);
                ratKingTextK2.gameObject.SetActive(true);
                
            }
            else
            {
                sightRange = textRange;
                attackRange = textRange;
                ratKingTextK1.gameObject.SetActive(false);
                ratKingTextK2.gameObject.SetActive(false);
                isTalking = false;
            }
        }
    }
}
