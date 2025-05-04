using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Members: Eric, Lucas, Thomas
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

    public ShotgunKB cone;
    
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
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        Vector3 targetPoint;

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(100f);
        }

        Vector3 shootDirection = (targetPoint - ShootPoint.position).normalized;


        for (int i = 0; i < pelletCount; i++)
        {
            
            float currentAngle = Random.Range(-spreadAngle/2, spreadAngle/2);
            float currentYAngle = Random.Range(-spreadAngle/2, spreadAngle/2);



            // Rotate direction by elevation (recoil)
            Quaternion elevation = Quaternion.AngleAxis(0, ShootPoint.right);      
            Quaternion horizontalRot = Quaternion.AngleAxis(currentAngle, ShootPoint.up);    // Left/right
            Quaternion verticalRot = Quaternion.AngleAxis(currentYAngle, ShootPoint.right);     // Up/down
            Vector3 spreadDirection = elevation * verticalRot * horizontalRot * ShootPoint.forward;

            // Add velocity to bullet
            GameObject bullet = Instantiate(Bullet, ShootPoint.position, Quaternion.LookRotation(spreadDirection));
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.velocity = bullet.transform.forward * bulletSpeed;
            




            ContactDamage bulletScript = bullet.GetComponent<ContactDamage>();
            if (bulletScript != null)
            {
                bulletScript.SetDamage(weaponDamage);
            }

            Destroy(bullet, 0.5f); 
        }
        // Apply backward recoil to player based on camera direction
        Vector3 kb = cam.transform.forward.normalized;
        move.rb.AddForce(kb * -10f, ForceMode.Impulse);

        cone.Fire();
        
        // Set projectile damage using the ContactDamage script
        

        if (audioSource != null) {
            audioSource.Play();
        }

       
    }
    
}
