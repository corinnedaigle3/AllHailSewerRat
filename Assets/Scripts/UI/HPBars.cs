using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBars : MonoBehaviour
{
    // References to different HP bar GameObjects
    public GameObject zeroHp; // Represents 0% HP
    public GameObject oneHp; // Represents 50% HP (or intermediate)
    public GameObject fiftyHp; // Represents 100% HP (full health)

    // Booleans to track Rat King's state
    public bool bTalkingDone; // Whether Rat King dialogue/cutscene is finished
    public bool whichHpBar; // Whether the player has cheese (affects HP bar display)

    public float hpBarTimer = 1f; // Timer used to switch between HP bars

    public GameObject ratKing; // Reference to the Rat King object
    RatKing rat; // Reference to RatKing script

    // Start is called before the first frame update
    void Start()
    {
        ratKing = GameObject.Find("RatKing"); // Find the Rat King object in scene

        // Initially hide all HP bars
        zeroHp.gameObject.SetActive(false);
        oneHp.gameObject.SetActive(false);
        fiftyHp.gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        // Get RatKing script and current states
        rat = ratKing.GetComponent<RatKing>();
        bTalkingDone = rat.talkingDone; // Check if talking/cutscene is finished
        whichHpBar = rat.DoorWithCheese; // Check if player has cheese

        // If talking is done and player has cheese
        if (bTalkingDone == true && whichHpBar == true)
        {
            hpBarTimer -= Time.deltaTime; // Countdown timer

            if (hpBarTimer > 0)
            {
                FHP(); // Show full HP bar initially
            }
            if (hpBarTimer <= 0)
            {
                OHP(); // Switch to intermediate HP bar
            }
        }

        // If talking is done but player does not have cheese
        if (bTalkingDone == true && whichHpBar == false)
        {
            FHP(); // Show full HP bar continuously
        }
    }

    // Update is called once per frame
    public void FHP()
    {
        // Display full HP bar
        zeroHp.gameObject.SetActive(false);
        oneHp.gameObject.SetActive(false);
        fiftyHp.gameObject.SetActive(true);
    }

    public void ZHP()
    {
        // Display zero HP bar
        zeroHp.gameObject.SetActive(true);
        oneHp.gameObject.SetActive(false);
        fiftyHp.gameObject.SetActive(false);
    }

    public void OHP()
    {
        // Display one/intermediate HP bar
        zeroHp.gameObject.SetActive(false);
        oneHp.gameObject.SetActive(true);
        fiftyHp.gameObject.SetActive(false);
    }
}
