using UnityEngine;

public class ContactDamage : MonoBehaviour
{
    private float damage;

    public void SetDamage(float newDamage)
    {
        damage = newDamage;
    }

    void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);

        Life life = other.GetComponentInParent<Life>();  
        if (life != null)
        {
            Debug.Log($"{other.name} has been hit with {damage} damage");
            life.take_Damage(damage); // Use the assigned damage value
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
        }
    }
}