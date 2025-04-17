using UnityEngine;
using UnityEngine.Events;

/**
 * Summary: Manages the health (life) of an entity and triggers death behavior.
 * Works for both player and non-player characters. Supports UnityEvents on death,
 * optional sound playback on damage, and custom player death logic via GameManager.
 */

public class Life : MonoBehaviour
{
    public float amount_max;
    public float amount;
    public UnityEvent onDeath;
    public bool isPlayer = false;
    private bool isDead = false;
    public AudioSource hitSound;

    void Start()
    {
        amount = amount_max;
    }

    // Method to apply damage to the entity
    public void take_Damage(float damage) 
    {
        if (isDead) return;
        amount -= damage;

        if (isPlayer) {
            //Debug.Log("Player Life: " + amount);
        }

        if (amount <= 0)
        {
            Die();
        }

        if (hitSound != null)
        {
            hitSound.Play();
        }
    }
    
    // Called when the life is depleted
    private void Die()
    {
        if (isDead) return;
        isDead = true;
        onDeath?.Invoke();

        // Notify the GameManager that the player has died (triggers UI, pause, etc.)
        if (isPlayer) {
            GameManager.Instance.PlayerDied();
        } else {
            Destroy(gameObject);
        }
    }
}
