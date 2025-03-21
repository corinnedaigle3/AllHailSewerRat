using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class PatrolRat : MonoBehaviour
{ 
    public LayerMask whatIsGround, whatIsPlayer;
    //public float health;

    [Header("Patroling")]
    public NavMeshAgent agent;
    public Transform patrolRoute; //waypoints
    public Transform player;
    public int t = 0;
    private Transform[] locations;

    [Header("States")]
    public float sightRange;
    public bool playerInSightRange;

    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        InitializePatrolRoute();
        MoveToNextPatrolLocation();
    }

    private void Update()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);

        if (!playerInSightRange && agent.remainingDistance < 0.2f) 
        { 
            MoveToNextPatrolLocation();
        }
        else 
        {
            ChasePlayer();
        }
    }

    void MoveToNextPatrolLocation() //enemy moves to next location
    {
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
        //make sure enemy doesn't move
        agent.SetDestination(player.transform.position);
    }

    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.name == "Player")
        {
            Debug.Log("PlayerCaught!");
            SceneManager.LoadScene("LoseScreen");
        }
    }

    // Update is called once per frame

    /*public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0) Invoke(nameof(DestroyEnemy), 0.5f);
        
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    } */
}
