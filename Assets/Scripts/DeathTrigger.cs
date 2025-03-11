using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class DeathTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Object entered Death Zone: " + other.name);
        if (other.CompareTag("Player"))  // Check if the player falls into the void
        {
            Debug.Log("Player hit the Death Zone! Triggering death screen...");
            FindFirstObjectByType<GameManager>().PlayerDied();

        }
    }
}
