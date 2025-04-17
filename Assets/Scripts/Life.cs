using UnityEngine;
using UnityEngine.Events;

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
    
    // Called when the life is dead
    private void Die()
    {
        if (isDead) return;
        isDead = true;
        onDeath?.Invoke();

        if (isPlayer) {
            GameManager.Instance.PlayerDied();
        } else {
            Destroy(gameObject);
        }
    }
}
