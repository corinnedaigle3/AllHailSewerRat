using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RatProjectile : MonoBehaviour
{
    public float loseScreenTimer = .25f; // Delay before triggering lose screen on collision

    // Update is called once per frame
    void Update()
    {
        // Empty, not used
    }

    private void OnCollisionEnter(Collision other)
    {
        loseScreenTimer -= Time.deltaTime; // Decrease timer when collision occurs

        if (loseScreenTimer <= 0)
        {
            Destroy(other.gameObject); // Destroy the object it collides with (likely the player)
            Destroy(gameObject); // Destroy the projectile
            SceneManager.LoadScene("LoseScreen"); // Load the lose screen scene
        }
    }
}