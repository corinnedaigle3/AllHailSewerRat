using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoingUP : MonoBehaviour
{
    private Transform goUp;

    // Start is called before the first frame update
    void Start()
    {
        goUp = GameObject.Find("GoingUp").transform;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
