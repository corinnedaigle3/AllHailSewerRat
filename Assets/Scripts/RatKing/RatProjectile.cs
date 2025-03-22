using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RatProjectile : MonoBehaviour
{
    public float loseScreenTimer = .27f;

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        loseScreenTimer -= Time.deltaTime;

        if (loseScreenTimer <= 0)
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
            SceneManager.LoadScene("LoseScreen");
        }
    }
}
