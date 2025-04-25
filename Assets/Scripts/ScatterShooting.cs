using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Summary: Handles a scatter-shot weapon that fires multiple projectiles ("pellets") with spread.
 * Applies recoil to the player, instantiates bullets, assigns velocity with randomized spread,
 * and sets projectile damage. Includes optional shooting sounds and cooldown via fireRate.
 */
public class ScatterShooting : MonoBehaviour
{
    public GameObject Bullet;
    public Transform ShootPoint;
    public float bulletSpeed;
    public float fireRate;
    public float weaponDamage;
    private float lastShootTime;
    public float spreadAngle;
    public int pelletCount;
    public Movement move;
    public Camera cam;
    public AudioSource audioSource;
    
    void Update()
    {
        // Fire only if mouse is clicked and cooldown has passed
        if (Input.GetButtonDown("Fire1"))
        {
            if (Time.time >= lastShootTime + (1f / fireRate)) {
                Shoot();
                lastShootTime = Time.time;
            }
        }
    }

    // Fires all pellets, applies recoil, and initializes bullet properties
    void Shoot()
    {
        for (int i = 0; i < pelletCount; i++)
        {
            GameObject bullet = Instantiate(Bullet, ShootPoint.position, ShootPoint.rotation);
            
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // Apply randomized horizontal and vertical spread within spreadAngle
                float currentAngle = Random.Range(-spreadAngle/2, spreadAngle/2);
                float currentYAngle = Random.Range(-spreadAngle/2, spreadAngle/2);
                //float angleStep = spreadAngle / (pelletCount - 1);
                //float currentAngle = -spreadAngle / 2 + (angleStep * i);
                
                // Rotate forward direction by spread offset
                Vector3 spreadDirection = Quaternion.Euler(currentYAngle, currentAngle, 0) * ShootPoint.forward;
                rb.velocity = spreadDirection * bulletSpeed;
            }
            // Apply backward recoil to player based on camera direction
            Vector3 kb = cam.transform.forward.normalized;
            move.rb.AddForce(kb * -3f, ForceMode.Impulse);
            
            // Set projectile damage using the ContactDamage script
            ContactDamage bulletScript = bullet.GetComponent<ContactDamage>();
            if (bulletScript != null)
            {
                bulletScript.SetDamage(weaponDamage);
            }

            if (audioSource != null) {
                audioSource.Play();
            }

            // Automatically destroy bullet after a short time to limit range
            Destroy(bullet, 0.5f); 
        }
    }
}
