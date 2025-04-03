using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject deathScreen; // Reference to the UI panel
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject); // Ensure only one instance exists
        }
    }

    public void PlayerDied()
    {
        deathScreen.SetActive(true); // Show death screen
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void RestartGame()
    {
        Debug.Log("Restart button clicked! Restarting scene...");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the level
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
