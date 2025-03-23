using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUp : MonoBehaviour
{
   public GameObject popUp;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!other.GetComponent<GotItem>().OpenDoorWithCheese && !other.GetComponent<GotItem>().OpenDoorWithKey)
            {
                popUp.SetActive(true);
             }
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
                popUp.SetActive(false);

    }


}
