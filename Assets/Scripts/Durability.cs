using UnityEngine;

public class Durability : MonoBehaviour
{
    public float maxHealth = 25f;
    private float currentHealth;
    private Rigidbody rb;
    public float radius = 1;
	public float force = 50f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage, Vector3 explosionOrigin, float forceOverride = -1, float radiusOverride = -1)
    {
        currentHealth -= damage;

        if (currentHealth <= 0f)
        {
            Break(explosionOrigin, forceOverride > 0 ? forceOverride : force,
                                     radiusOverride > 0 ? radiusOverride : radius);
        }
    }

    private void Break(Vector3 origin, float force, float radius)
    {
        // Unfreeze this brick
        rb.isKinematic = false;
        rb.AddExplosionForce(force, origin, radius);

        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider hit in colliders)
        {
            Rigidbody otherRb = hit.attachedRigidbody;

            if (otherRb != null && otherRb != rb) // Don't apply to self again
            {
                otherRb.isKinematic = false;
                otherRb.AddExplosionForce(force, origin, radius);
            }
        }
    }
}
