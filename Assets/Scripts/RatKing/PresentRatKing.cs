using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PresentRatKing : MonoBehaviour
{
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;

    public float sightRange;
    public bool playerInSightRange;

    public TextMeshProUGUI presentTextP1;
    private float textTimeP = 3f;

    // Start is called before the first frame update
    void Awake()
    {
        transform.LookAt(player);
    }

    // Update is called once per frame
    void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);

        if (playerInSightRange)
        {
            textTimeP -= Time.deltaTime;

            if (textTimeP >= 1f)
            {
                presentTextP1.gameObject.SetActive(true);
            }
            else
            {
                presentTextP1.gameObject.SetActive(false);
                SceneManager.LoadScene("WinScreen");
            }
        }
    }
}
