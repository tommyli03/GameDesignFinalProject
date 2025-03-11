using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject deathScreen; // Reference to the UI panel

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
