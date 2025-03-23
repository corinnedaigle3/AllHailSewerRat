using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.U2D;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainCreditsUI : MonoBehaviour
{
    public GameObject credits;
    public GameObject start;
    public GameObject quit;
    public GameObject back;
    public GameObject mainMenu;
    public GameObject creditsMenu;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        
        credits.gameObject.SetActive(true);
        start.gameObject.SetActive(true);
        quit.gameObject.SetActive(true);
        back.gameObject.SetActive(false);

        mainMenu.gameObject.SetActive(true);
        creditsMenu.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play()
    {
        SceneManager.LoadScene("Game");
    }

    public void BackToMenu()
    {
        credits.gameObject.SetActive(true);
        start.gameObject.SetActive(true);
        quit.gameObject.SetActive(true);
        back.gameObject.SetActive(false);

        mainMenu.gameObject.SetActive(true);
        creditsMenu.gameObject.SetActive(false);
    }


    public void Esc()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Credits()
    {
        credits.gameObject.SetActive(false);
        start.gameObject.SetActive(false);
        quit.gameObject.SetActive(false);
        back.gameObject.SetActive(true);

        mainMenu.gameObject.SetActive(false);
        creditsMenu.gameObject.SetActive(true);
    }
}
