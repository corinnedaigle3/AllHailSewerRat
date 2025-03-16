using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolRat : MonoBehaviour
{
    public Transform patrolRoute; //waypoints
    public Transform pills; //Player

    public float speed = 2f;
    public int t = 0;
    private NavMeshAgent agent;
    private Transform[] locations;
    private bool chasingPlayer = false;

    void Start()
    {

        //swapped vectors out for waypoints
        agent = GetComponent<NavMeshAgent>();
        // Set different colors for each object
        InitializePatrolRoute();
        MoveToNextPatrolLocation();


    }

    void Update()
    {


        //locate player and chase if within close proximity
        if (!chasingPlayer && !agent.pathPending && agent.remainingDistance < 0.2f)
        {
            MoveToNextPatrolLocation();
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


    void OnCollisionEnter(Collision collision) //method recognizes collision
    {
        if (collision.gameObject.name == "Player")
        {
            Debug.Log("Halt!");
        }

        if (collision.gameObject.name == "Player")
        {
            Debug.Log("Player detected - start chasing!");
            chasingPlayer = true;
            agent.SetDestination(pills.position);
        }
    }

    void OnCollisionExit(Collision collision) //method for end of collision
    {
        if (collision.gameObject.name == "Player")
        {
            Debug.Log("Player out of range - resume patrol.");
            chasingPlayer = false;
            MoveToNextPatrolLocation();
        }
    }
}
