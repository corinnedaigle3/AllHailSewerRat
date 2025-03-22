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
    public NavMeshAgent agent;
    public Transform patrolRoute; //waypoints
    public Transform player;
    public int t = 0;
    private Transform[] locations;

    [Header("States")]
    public int sightRange;
    public bool playerInSightRange;

    Vector3 velocity = Vector3.zero;

    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        InitializePatrolRoute();
        MoveToNextPatrolLocation();
    }

    private void FixedUpdate()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);

        if (!playerInSightRange && agent.remainingDistance < 0.2f) 
        { 
            MoveToNextPatrolLocation();
        }
        
        if (playerInSightRange)
        {
            ChasePlayer();
        }
    }

    void MoveToNextPatrolLocation() //enemy moves to next location
    {
        agent.updatePosition = true;

        if (locations.Length == 0) return;
        {
            agent.SetDestination(locations[t].position);
            t = (t + 1) % locations.Length;
        }
    }

    void InitializePatrolRoute()//method initialized patrol route
    {
        locations = new Transform[patrolRoute.childCount];
        for (int i = 0; i < patrolRoute.childCount; i++)
        {
            locations[i] = patrolRoute.GetChild(i);
        }
    }

    private void ChasePlayer()
    {
        agent.updatePosition = false;
        agent.SetDestination(player.position);
        transform.position = Vector3.SmoothDamp(transform.position, agent.nextPosition, ref velocity, 0.1f);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            p = collision.gameObject.GetComponent<PlayerMovement>();
            p.dead = true;
            Debug.Log("PlayerDead " + p.dead );

            StartCoroutine(pLose(1f));
            Debug.Log("PlayerCaught!");
        }
    }

    IEnumerator pLose(float waitTime)
    {

        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene("LoseScreen");

    }
}
