using UnityEngine;

public class onTrigger : MonoBehaviour
{
    public bool OnUp = false;
    public bool OnDown = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && gameObject.CompareTag("GoingUP"))
        {
            OnUp = true;
        }
        if (other.CompareTag("Player") && gameObject.CompareTag("GoingDown"))
        {
            OnDown = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && gameObject.CompareTag("GoingUP"))
        {
            OnUp = false;
        }
        if (other.CompareTag("Player") && gameObject.CompareTag("GoingDown"))
        {
            OnDown = false;
        }
    }

}
