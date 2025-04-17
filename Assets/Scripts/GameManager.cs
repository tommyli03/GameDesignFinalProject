using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

/**
 * Summary: Manages game-wide state, including player death and game restarts.
 * Handles pausing the game, displaying the death UI, and restarting the current level.
 * Also ensures a single instance of the GameManager persists using the Singleton pattern.
 */
public class GameManager : MonoBehaviour
{
    // Reference to the UI panel
    public GameObject deathScreen;
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject); // Ensure only one instance exists
        }
    }

    // Called when the player dies (e.g., from falling or fatal damage)
    public void PlayerDied()
    {
        Time.timeScale = 0f;
        deathScreen.SetActive(true); 
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Called by UI button to restart the level after death
    public void RestartGame()
    {
        Debug.Log("Restart button clicked! Restarting scene...");
        // Reload the level
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f;
    }
}
