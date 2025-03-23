using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HPBars : MonoBehaviour
{
    public GameObject zeroHp;
    public GameObject oneHp;
    public GameObject fiftyHp;

    public bool bTalkingDone;
    public bool whichHpBar;

    public float hpBarTimer = 1f;

    public GameObject ratKing;
    RatKing rat;

    // Start is called before the first frame update
    void Start()
    {
        ratKing = GameObject.Find("RatKing");

        zeroHp.gameObject.SetActive(false);
        oneHp.gameObject.SetActive(false);
        fiftyHp.gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        

        rat = ratKing.GetComponent<RatKing>();
        bTalkingDone = rat.talkingDone;
        whichHpBar = rat.DoorWithCheese;

        if (bTalkingDone == true && whichHpBar == true)
        {
            hpBarTimer -= Time.deltaTime;

            if (hpBarTimer > 0)
            {
                FHP(); 
            }
            if (hpBarTimer <= 0)
            {
                OHP();
            }
        }

        if (bTalkingDone == true && whichHpBar == false)
        {
            FHP();
        }
    }

    // Update is called once per frame
    public void FHP()
    {
        zeroHp.gameObject.SetActive(false);
        oneHp.gameObject.SetActive(false);
        fiftyHp.gameObject.SetActive(true);
    }

    public void ZHP()
    {
        zeroHp.gameObject.SetActive(true);
        oneHp.gameObject.SetActive(false);
        fiftyHp.gameObject.SetActive(false);
    }

    public void OHP()
    {
        zeroHp.gameObject.SetActive(false);
        oneHp.gameObject.SetActive(true);
        fiftyHp.gameObject.SetActive(false);
    }
}
