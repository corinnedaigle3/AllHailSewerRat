using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    public GameObject player;
    public bool DoorWithCheese;
    GotItem cheese;

    public GameObject ratKing;
    public bool textSight;
    RatKing rat;

    public AudioSource bM1;
    public AudioSource bM2;

    // Start is called before the first frame update
    void Awake()
    {
        player = player = GameObject.Find("Player");
        ratKing = GameObject.Find("RatKing");

        bM1.Play();
        bM2.Play();
    }

    // Update is called once per frame
    void Update()
    {
        cheese = player.GetComponent<GotItem>();
        DoorWithCheese = cheese.OpenDoorWithCheese;
        rat = ratKing.GetComponent<RatKing>();
        textSight = rat.playerInTextRange;

        if (DoorWithCheese == true && textSight == true)
        {
            bM1.Stop();
            bM2.Stop();
        }

        if (DoorWithCheese == false && textSight == false)
        {
            bM1.Play();
            bM2.Play();
        }
    }
}
