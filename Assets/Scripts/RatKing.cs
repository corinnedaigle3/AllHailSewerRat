using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatKing : MonoBehaviour
{
    public Rigidbody rb;
    public LayerMask playerLayer;

    public float playerDistance;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        CheckForPlayer();
        

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = new Vector3(speed, rb.velocity.y, rb.velocity.z);
    }

    void CheckForPlayer()
    {
        Vector3 direction = transform.forward;

        RaycastHit hitPlayer;
        Physics.Raycast(transform.position, direction, out hitPlayer, playerDistance, playerLayer);

        if (hitPlayer.collider == true)
        {

        }
    }

    IEnumerable PlayerDetected()
    {
        
    }
}
