using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class Projectile : MonoBehaviour
{
    Rigidbody rb; // Rigidbody for physics movement
    private GameObject lookAtPoint; // Reference to the point the projectile should travel toward
    public float shootingForce = 1000f; // Force applied to the projectile

    private void Awake()
    {
        lookAtPoint = GameObject.Find("LookAtPoint"); // Find the target object
        rb = GetComponent<Rigidbody>(); // Get Rigidbody component
    }

    void Start()
    {
        // Get the forward direction of the lookAtPoint and apply force
        Vector3 shootingDirection = lookAtPoint.transform.forward;

        rb.AddForce(shootingDirection * shootingForce); // Launch projectile

        Debug.Log("Shooting Direction is " + shootingDirection); // Debug the direction
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check collision with Rat King
        if (collision.gameObject.CompareTag("RatKing"))
        {
            Destroy(collision.gameObject); // Destroy the Rat King
            Destroy(gameObject); // Destroy the projectile
        }

        // Check collision with any other enemy
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject); // Destroy the enemy
            Destroy(gameObject); // Destroy the projectile
        }

        // Self-destruct after a short delay (in case it didn't hit an enemy)
        StartCoroutine(SelfDestruction(.5f));
    }

    IEnumerator SelfDestruction(float waitTime)
    {
        yield return new WaitForSeconds(waitTime); // Wait for the specified time
        Destroy(gameObject); // Destroy the projectile
    }
}