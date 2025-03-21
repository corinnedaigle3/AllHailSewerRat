using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GotItem : MonoBehaviour
{
    private int keyCount = 0;
    private int cheeseCount = 0;
    public bool OpenDoorWithKey;
    public bool OpenDoorWithCheese;


    void Start()
    {
        OpenDoorWithKey = false;
        OpenDoorWithCheese = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Key"))
        {
            Destroy(other.gameObject);
            keyCount++;

            if (keyCount >= 3)
            {
                OpenDoorWithKey = true;
            }
            else
            {
                Debug.Log("Keys collected: " + keyCount);
            }
        }

        if (other.CompareTag("Cheese"))
        {
            Destroy(other.gameObject);
            cheeseCount++;
            OpenDoorWithCheese = true;
        }
    }
}
