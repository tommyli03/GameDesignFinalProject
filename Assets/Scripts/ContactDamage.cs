using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

/**
    * Members: Eric, Lucas, Thomas
    * Summary: Handles collision-based damage interactions for projectiles or hazards.
    * When this object collides with a player or destructible object, it applies damage,
    * plays a visual effect, and triggers a temporary screen post-processing effect.
    * Also supports damaging both player characters (Life component) and destructible props (Durability component).
*/
public class ContactDamage : MonoBehaviour
{
    private float damage;
    public GameObject hitEffectPrefab;
    public Volume hitVolume;
    public float volFade = 5f;
    private float targetVolumeWeight = 0f;
    public float fxlength;
    
    public float duration = 0f;

    // Allows external scripts to set the damage value before collision occurs
    public void SetDamage(float newDamage)
    {
        damage = newDamage;
    }

    // Smoothly fades the post-processing volume toward the target weight
    void Update()
    {
        if (hitVolume != null)
        {
            hitVolume.weight = Mathf.Lerp(hitVolume.weight, targetVolumeWeight, volFade * Time.deltaTime);
        }
    }

    // Coroutine to enable a temporary visual effect (like screen bleed), then fade it out
    System.Collections.IEnumerator Bleed()
    {
        float startTime = Time.time;
        targetVolumeWeight = 1f;

        while (Time.time < startTime + fxlength)
        {
            yield return null; // Wait for the next frame
        }

        targetVolumeWeight = 0f;
        
    }

    // Triggered when this object enters a collider
    void OnTriggerEnter(Collider other)
    {

        if (other.GetComponentInParent<Movement>() != null)
            StartCoroutine(Bleed());

        
        StartCoroutine(DelayedDestroy());
        // Handle damage to player-like objects with a Life component
        Life life = other.GetComponentInParent<Life>();  
        if (life != null)
        {
            Debug.Log($"{other.name} has been hit with {damage} damage");
            life.take_Damage(damage); // Use the assigned damage value
            if (hitEffectPrefab != null)
            {
                Instantiate(hitEffectPrefab, transform.position, Quaternion.identity);
            }
        }

        // Handle damage to destructible environment objects (e.g., bricks)
        Durability brick = other.GetComponent<Durability>();
        Debug.Log("brick!");
        if (brick == null)
        {
            Debug.Log("brick is null");
        }
        if (brick != null)
        {
            Debug.Log($"{other.name} has been hit with {damage} damage (Brick)");
            brick.TakeDamage(damage, transform.position, 100f, 3f); // use reasonable explosion force/radius
            if (hitEffectPrefab != null)
            {
                Instantiate(hitEffectPrefab, transform.position, Quaternion.identity);
            }
        }

        
    }


    System.Collections.IEnumerator DelayedDestroy() //here in case you want the bullet to continue going for a little (only in use for sniper)
    {
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }
}