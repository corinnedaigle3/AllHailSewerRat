using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject player;
    public GameObject openDoor;
    private GotKey gotKey;

    void Start()
    {
        if (player != null)
        {
            gotKey = player.GetComponent<GotKey>();
        }

/*        if (gotKey == null)
        {
            Debug.Log("GotKey component not found on Player!");
        }*/
    }

    public void DoorDisappear()
    {
        if (gotKey.OpenDoorWithKey)
        {
            gameObject.SetActive(false);
            openDoor.SetActive(true);
        }
        else
        {
            Debug.Log("Door cannot be opened.");
        }
    }
}
