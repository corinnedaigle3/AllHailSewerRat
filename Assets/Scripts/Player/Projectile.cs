using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class Projectile : MonoBehaviour
{
    Rigidbody rb;
    public Camera cam;
    public float shootingForce = 1000f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        // Get the camera's forward direction and apply force to the projectile
        Vector3 shootingDirection = new Vector3(cam.transform.forward.x, 1f, cam.transform.forward.z).normalized;

        rb.AddForce(shootingDirection * shootingForce);

        StartCoroutine(SelfDestruction(3f));
    }

    IEnumerator SelfDestruction(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Destroy(gameObject);
    }
}
