using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelOrientation : MonoBehaviour
{
    PlayerMovement pMove;
    Vector3 moveDirection;
    
    // Start is called before the first frame update
    void Start()
    {
        pMove = GetComponentInParent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
         moveDirection = transform.forward * pMove.vInput * pMove.hInput;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDirection), 1f * Time.deltaTime);
    }
}
