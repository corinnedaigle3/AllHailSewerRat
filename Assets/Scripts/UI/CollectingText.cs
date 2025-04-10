using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectingText : MonoBehaviour
{
    GameObject player;
    GotItem g;
    public GameObject cheeseText;
    public GameObject keyText;
    bool stop = false;
    

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        g = player.GetComponent<GotItem>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (g.OpenDoorWithCheese && !stop)
        {
            StartCoroutine(collectionText(5f, cheeseText));

        }

        if (g.OpenDoorWithKey && !stop)
        {
            StartCoroutine(collectionText(5f, keyText));
        }
    }

    IEnumerator collectionText(float waitTime, GameObject game)
    {
        game.SetActive(true);
        yield return new WaitForSeconds(waitTime);
        game.SetActive(false);
        stop = true;
    }
}
