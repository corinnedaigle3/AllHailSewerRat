using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseScript : MonoBehaviour
{

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("Game");
        } else if (Input.GetKeyDown(KeyCode.P))
        {
            SceneManager.LoadScene("MainMenu");

        }
    }
}
