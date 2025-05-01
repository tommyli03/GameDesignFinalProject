using UnityEngine;
using UnityEngine.Events;

/**
 * Members: Eric, Lucas, Thomas
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
    public GameObject healthPackPrefab;
    [Range(0f, 1f)] public float healthDropChance = 1.0f;
    public AudioClip pickupSound;

    private Vector2 healthBarSize = new Vector2(0.75f, 0.1f); // World units
    private Vector3 healthBarOffset = new Vector3(0, 2.5f, 0); // Above the enemy
    private float healthBarVisibleRange = 30f; // Within this range, the health bar will be visible

    void Start()
    {
        amount = amount_max;
        if (!isPlayer)
        {
            GameManager.Instance.RegisterEnemy();
        }
    }

    // Method to apply damage to the entity
    public void take_Damage(float damage)
    {
        if (isDead) return;
        amount -= damage;

        if (isPlayer)
        {
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
        if (isPlayer)
        {
            GameManager.Instance.PlayerDied();
        }
        else
        {
            GameObject thisEnemy = this.gameObject;

            // Check if this is a SniperDrop enemy
            if (thisEnemy.CompareTag("Enemy") &&
                thisEnemy.layer == LayerMask.NameToLayer("SniperDrop"))
            {
                GameObject player = GameObject.FindWithTag("Player");
                if (player != null)
                {
                    Swap_Weapons swap = player.GetComponent<Swap_Weapons>();
                    if (swap != null)
                    {
                        swap.sniperUnlocked = true;

                        int sniperIndex = swap.FindSniperIndex();
                        if (sniperIndex != -1)
                            swap.weapons[sniperIndex].SetActive(true);

                        UIManager.Instance?.ShowMessage("Nice job! You found the sniper gun. You can now swap to it.");
                    }
                }
            }
            TryDropHealthPack();
            GameManager.Instance.EnemyDied();
            Destroy(gameObject);
        }
    }

    private void TryDropHealthPack()
    {
        if (healthPackPrefab != null && Random.value < healthDropChance)
        {
            Instantiate(healthPackPrefab, transform.position + Vector3.up * 0.3f, Quaternion.identity);
        }
    }

    // Draw health bar only for enemies
    private void OnGUI()
    {
        if (isPlayer) return; // No health bar for player

        float distance = Vector3.Distance(Camera.main.transform.position, transform.position);

        if (distance > healthBarVisibleRange) return; // Don't draw healthbars if too far

        Vector3 worldPosition = transform.position + healthBarOffset;
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(worldPosition);

        if (screenPosition.z > 0) // Only draw if in front of camera
        {
            float healthPercent = Mathf.Clamp01(amount / amount_max);
            float barWidth = healthBarSize.x * 100; // Convert to pixels
            float barHeight = healthBarSize.y * 100;

            // Convert world size to screen space (approximate)
            Vector2 barPosition = new Vector2(screenPosition.x - (barWidth / 2), Screen.height - screenPosition.y - (barHeight / 2));

            // Background (red)
            GUI.color = Color.red;
            GUI.DrawTexture(new Rect(barPosition.x, barPosition.y, barWidth, barHeight), Texture2D.whiteTexture);

            // Foreground (green)
            GUI.color = Color.green;
            GUI.DrawTexture(new Rect(barPosition.x, barPosition.y, barWidth * healthPercent, barHeight), Texture2D.whiteTexture);

            GUI.color = Color.white; // Reset GUI color
        }
    }

}
