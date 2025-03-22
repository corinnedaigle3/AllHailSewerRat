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

    // Start is called before the first frame update
    void Start()
    {
        player = player = GameObject.Find("Player");
        ratKing = GameObject.Find("RatKing");
    }

    // Update is called once per frame
    void Update()
    {
        cheese = player.GetComponent<GotItem>();
        DoorWithCheese = cheese.OpenDoorWithCheese;
        rat = ratKing.GetComponent<RatKing>();
        textSight = rat.playerInSightRange;
    }
}
