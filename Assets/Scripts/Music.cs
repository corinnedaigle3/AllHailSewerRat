using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    public GameObject player;
    public bool DoorWithCheese = false;
    GotItem cheese;

    public GameObject ratKing;
    public bool textSight = false;
    RatKing rat;

    public AudioSource bM1;
    public AudioSource bM2;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        ratKing = GameObject.Find("RatKing");

        bM1.Play();
        bM2.Play();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        cheese = player.GetComponent<GotItem>();
        DoorWithCheese = cheese.OpenDoorWithCheese;
        rat = ratKing.GetComponent<RatKing>();
        textSight = rat.playerInTextRange;

        if (DoorWithCheese == true && textSight == true)
        {
            bM1.Pause();
            bM2.Pause();
        }
        else
        { 
            bM1.Play();
            bM2.Play();
        }
    }
}
