using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GotItem : MonoBehaviour
{
    private int keyCount = 0;
    private int cheeseCount = 0;
    public int buttonCount = 0;
    public bool OpenDoorWithKey;
    public bool OpenDoorWithCheese;
    public bool OpenMiniDoor;

    public PlayerMovement playerMovement;
    private GameObject Buttons;
    private ButtonsKey ButtonsKey;

    void Start()
    {
        OpenDoorWithKey = false;
        OpenDoorWithCheese = false;
        OpenMiniDoor = false;

        Buttons = GameObject.FindWithTag("ButtonsKey");
        ButtonsKey = Buttons.GetComponent<ButtonsKey>();
    }

    private void OnTriggerEnter(Collider other)
    {


        if (other.CompareTag("Key"))
        {
            Destroy(other.gameObject);
            keyCount++;
            OpenDoorWithKey = true;
        }

        if (other.CompareTag("Cheese"))
        {
            Destroy(other.gameObject);
            cheeseCount++;
            OpenDoorWithCheese = true;
            playerMovement.readyToShoot = true;
            Debug.Log("Cheese collected: " + cheeseCount);

        }
    }

    private void Update()
    {
        if (buttonCount == 3)
        {
            OpenMiniDoor = true;
        }
    }
}