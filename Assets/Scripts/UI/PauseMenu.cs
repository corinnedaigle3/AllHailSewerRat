using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu; // Reference to the UI panel for the pause menu
    public static bool isPaused; // Tracks whether the game is currently paused

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false); // Ensure pause menu is hidden at game start
    }

    void Update()
    {
        // Toggle pause when player presses "P"
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (isPaused)
            {
                ResumeGame(); // Unpause if already paused
            }
            else
            {
                PauseGame(); // Pause if not paused
            }
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true); // Show pause menu UI
        Time.timeScale = 0f; // Freeze all in-game physics and movement
        isPaused = true;

        // Unlock the cursor so the player can click UI buttons
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false); // Hide pause menu UI
        Time.timeScale = 1f; // Resume game time
        isPaused = false;

        // Lock the cursor back to the center for gameplay
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu"); // Load the main menu scene
    }

    public void Quit()
    {
        Application.Quit(); // Close the application/game
    }
}
