using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPoint : MonoBehaviour
{
    public Camera cam;
    public float distanceFromCamera = 5f;

    void Update()
    {
        // Get the direction opposite to the camera's forward vector
        Vector3 oppositeSide = cam.transform.forward;

        // Position the point at a fixed distance from the camera, parallel to its view
        transform.position = cam.transform.position + oppositeSide * distanceFromCamera;

        Debug.Log(oppositeSide);
    }
}
