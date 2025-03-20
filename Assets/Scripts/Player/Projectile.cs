using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Windows;

public class Projectile : MonoBehaviour
{
    Rigidbody rb;
    private GameObject lookAtPoint;
    public float shootingForce = 1000f;

    private void Awake()
    {
        lookAtPoint = GameObject.Find("LookAtPoint");
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        Debug.Log("Look at point position: " + lookAtPoint.transform.position);
        Debug.Log("The look at Postion " + lookAtPoint.transform.position + " and The transform position " + transform.position);
        // Get the camera's forward direction and apply force to the projectile
        Vector3 shootingDirection = lookAtPoint.transform.forward;

        rb.AddForce(shootingDirection * shootingForce);

        Debug.Log("Shooting Direction is " + shootingDirection);
    }

    public void OnCollisionEnter()
    {
        StartCoroutine(SelfDestruction(.2f));

    }
    IEnumerator SelfDestruction(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Destroy(gameObject);
    }
}
