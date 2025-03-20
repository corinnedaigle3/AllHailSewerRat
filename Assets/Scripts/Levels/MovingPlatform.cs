using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    private onTrigger trigger;
    private Vector3 Original_platform;
    public float speed =5f;

    void Start()
    {
        Original_platform = transform.position;
        trigger = GetComponent<onTrigger>(); // Get the trigger component from THIS platform

        if (trigger == null)
        {
            //Debug.LogError(gameObject.name);
        }
    }

    void Update()
    {
        if (trigger != null && trigger.OnUp)
        {
            PlatformUP();
        }
        else if (trigger != null && trigger.OnDown)
        {
            PlatformDown();
        }

    }

    void PlatformUP()
    {
        transform.position += Vector3.up * speed * Time.deltaTime;
    }

    void PlatformDown()
    {
        transform.position += Vector3.down * speed * Time.deltaTime;
    }
}
