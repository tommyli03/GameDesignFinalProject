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

        Debug.Log("Current damage: " + damage);

        Life life = other.GetComponent<Life>();
        if (life != null)
        {
            life.amount -= damage; // Use the assigned damage value
        }
    }
}