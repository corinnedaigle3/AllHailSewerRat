using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{

    PlayerMovement pMovement;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        pMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("inGround", pMovement.isGround); 
        animator.SetBool("isMoving", pMovement.isMoving);
        animator.SetBool("isJumping", pMovement.isJumping);
        animator.SetBool("Dead", pMovement.dead);

        if (pMovement.isJumping)
            StartCoroutine(changeBool(1f));
    }
    IEnumerator changeBool(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        pMovement.isJumping = false;
    }
    
}
