using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class Projectile : MonoBehaviour
{
    Rigidbody rb;
    public GameObject lookAtPoint;
    public float shootingForce = 1000f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        // Get the camera's forward direction and apply force to the projectile
        Vector3 shootingDirection = (lookAtPoint.transform.position - transform.position).normalized;

        rb.AddForce(shootingDirection * shootingForce, ForceMode.Impulse);

        StartCoroutine(SelfDestruction(3f));
    }

    IEnumerator SelfDestruction(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Destroy(gameObject);
    }
}
