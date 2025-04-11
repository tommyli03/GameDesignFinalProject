using UnityEngine;

using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ContactDamage : MonoBehaviour
{
    private float damage;
    public GameObject hitEffectPrefab;

    public Volume hitVolume;
    public float volFade = 5f;

    private float targetVolumeWeight = 0f;
    public float fxlength;

    public void SetDamage(float newDamage)
    {
        damage = newDamage;

    }


    void Update()
    {
        if (hitVolume != null)
        {
            hitVolume.weight = Mathf.Lerp(hitVolume.weight, targetVolumeWeight, volFade * Time.deltaTime);
        }
    }

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

    void OnTriggerEnter(Collider other)
    {

        if (other.GetComponentInParent<Movement>() != null)
            StartCoroutine(Bleed());

        Destroy(gameObject);
        

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
}