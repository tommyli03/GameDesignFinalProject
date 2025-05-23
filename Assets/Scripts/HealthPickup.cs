using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Members: Eric, Lucas, Thomas
 * Summary: Heals the player upon contact and plays a pickup sound effect.
 * Searches for a Life component in the player object or its hierarchy.
 * Destroys the pickup object after healing is applied.
 */
public class HealthPickup : MonoBehaviour
{
    public float healAmount = 25f;
    public AudioClip pickupSound;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger entered by: " + other.name);

        if (other.CompareTag("PlayerBody"))
        {
            Debug.Log("Player detected, attempting heal.");

            Life life = other.GetComponent<Life>();
            if (life == null)
                life = other.GetComponentInChildren<Life>();
            if (life == null)
                life = other.GetComponentInParent<Life>();
            if (life != null)
            {
                Debug.Log("Healing...");
                life.amount = Mathf.Min(life.amount + healAmount, life.amount_max);

                if (pickupSound != null)
                {
                    AudioSource.PlayClipAtPoint(pickupSound, transform.position);
                }

                Destroy(gameObject);
            }
            else
            {
                Debug.Log("No Life script found!");
            }
        }
    }
}
