using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

/**
 * Summary: Detects when the player or other objects fall into a "death zone" (e.g., off the map).
 * If the player enters the trigger zone, this script calls the GameManager to initiate the death sequence.
 */
public class DeathTrigger : MonoBehaviour
{
    // Triggered automatically when any collider enters the death zone's trigger collider
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Object entered Death Zone: " + other.name);
        
        // Only respond to the player falling into the zone
        if (other.CompareTag("Player"))  
        {
            Debug.Log("Player hit the Death Zone! Triggering death screen...");
            FindFirstObjectByType<GameManager>().PlayerDied();
        }
    }
}
