using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsKey : MonoBehaviour
{

    private GotItem GotItem;
    public GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        GotItem = player.GetComponent<GotItem>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ButtonCount()
    {
        GotItem.buttonCount++;
    }
}