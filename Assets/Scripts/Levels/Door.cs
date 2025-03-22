using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject player;
    //public GameObject openDoor;
    private GotItem gotItem;
    public AudioSource doorCheese;
    public AudioSource doorKey;

    void Start()
    {
        if (player != null)
        {
            gotItem = player.GetComponent<GotItem>();
        }

/*        if (gotKey == null)
        {
            Debug.Log("GotKey component not found on Player!");
        }*/
    }

    public void DoorWithKey()
    {
        if (gotItem.OpenDoorWithKey)
        {
            gameObject.SetActive(false);
            //openDoor.SetActive(true);
            Debug.Log("Door opened.");
            doorKey.Play();
        }
        else
        {
            Debug.Log("Door cannot be opened.");

        }
    }

    public void MiniDoor()
    {
        if (gotItem.OpenMiniDoorWithKey)
        {
            gameObject.SetActive(false);
            Debug.Log("Door opened.");

        }
    }

    public void DoorWithCheese()
    {
        if (gotItem.OpenDoorWithCheese)
        {
            gameObject.SetActive(false);
            //openDoor.SetActive(true);
            Debug.Log("Door opened.");
            doorCheese.Play();
        }
        else
        {
            Debug.Log("Door cannot be opened.");
        }
    }
}
