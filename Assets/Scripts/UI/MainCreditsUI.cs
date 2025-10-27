using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainCreditsUI : MonoBehaviour
{
    // UI elements
    public GameObject credits; // Text or panel showing credits
    public GameObject start; // Start button
    public GameObject quit; // Quit button
    public GameObject back; // Back button (from credits)
    public GameObject mainMenu; // Main menu panel
    public GameObject creditsMenu; // Credits panel

    // Start is called before the first frame update
    void Start()
    {
        // Unlock cursor and make it visible
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Set initial UI visibility
        credits.gameObject.SetActive(true); // Show title or credits text
        start.gameObject.SetActive(true); // Show start button
        quit.gameObject.SetActive(true); // Show quit button
        back.gameObject.SetActive(false); // Hide back button initially

        mainMenu.gameObject.SetActive(true); // Show main menu panel
        creditsMenu.gameObject.SetActive(false); // Hide credits panel initially
    }

    // Update is called once per frame
    void Update()
    {
        // Currently unused; could handle keyboard shortcuts if needed
    }

    // Load the main game scene
    public void Play()
    {
        SceneManager.LoadScene("Game");
    }

    // Return from credits view to main menu view
    public void BackToMenu()
    {
        credits.gameObject.SetActive(true); // Show title
        start.gameObject.SetActive(true); // Show start button
        quit.gameObject.SetActive(true); // Show quit button
        back.gameObject.SetActive(false); // Hide back button

        mainMenu.gameObject.SetActive(true); // Show main menu panel
        creditsMenu.gameObject.SetActive(false); // Hide credits panel
    }

    // Exit the game if Escape key is pressed
    public void Esc()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    // Exit the game (called from quit button)
    public void Quit()
    {
        Application.Quit();
    }

    // Show credits panel and hide main menu buttons
    public void Credits()
    {
        credits.gameObject.SetActive(false); // Hide title
        start.gameObject.SetActive(false); // Hide start button
        quit.gameObject.SetActive(false); // Hide quit button
        back.gameObject.SetActive(true); // Show back button

        mainMenu.gameObject.SetActive(false); // Hide main menu panel
        creditsMenu.gameObject.SetActive(true); // Show credits panel
    }
}
