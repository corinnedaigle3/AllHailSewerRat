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
     
        transform.rotation = Quaternion.Slerp(transform.rotation, 
            Quaternion.LookRotation(pMove.moveDirection),
            10f * Time.deltaTime);
    }
}
