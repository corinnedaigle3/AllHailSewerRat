using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GotKey : MonoBehaviour
{
    private int keyCount = 0;
    public bool OpenDoorWithKey;

    void Start()
    {
        OpenDoorWithKey = false;
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
    }
}
