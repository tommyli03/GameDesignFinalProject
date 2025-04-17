using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Summary: Handles single-shot or automatic weapon shooting mechanics.
 * Simulates recoil, spread, bullet velocity, visual and audio effects.
 * Shoots a ray to determine the bullet's target, applies spread in both X/Y axes,
 * and instantiates a physical projectile.
 */
public class Shooting : MonoBehaviour
{
    public ParticleSystem muzzleFlash;
    public Camera playerCamera;
    public GameObject Bullet;
    public Transform ShootPoint;
    public float bulletSpeed;
    public float fireRate;
    private float lastShootTime;
    public float weaponDamage;
    public float spreadAngle;
    public float recoil;
    public Camera cam;
    public float rAngle = 0f; 
    public AudioSource shootAudio;

    void Update()
    {
        // Check for firing input and cooldown
        if (Input.GetMouseButton(0))
        {
            if (Time.time >= lastShootTime + (1f / fireRate)) {
                Shoot();
                lastShootTime = Time.time;
            }
        }
        // Recoil recovery (gradually lowers the aim back to neutral)
        if (rAngle > 0f)
        {
            if (rAngle < 2f)
                rAngle -= .5f;
            rAngle *= .8f;
        }
    }

    // Fires one bullet with spread and visual/audio feedback
    void Shoot()
    {
        // Cast a ray from the center of the screen to find target
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
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

        // Add spread to direction
        float spreadX = Random.Range(-spreadAngle / 2f, spreadAngle / 2f);
        float spreadY = Random.Range(-spreadAngle / 2f, spreadAngle / 2f);
        Quaternion spreadRotation = Quaternion.Euler(spreadY + rAngle, spreadX, 0); //This line and the next sets the direction of one bullet as the camera angle, plus a slight random rotation.
        Vector3 finalDirection = spreadRotation * shootDirection;

        GameObject bullet = Instantiate(Bullet, ShootPoint.position, Quaternion.LookRotation(finalDirection));

        // Add velocity to bullet
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            float currentAngle = Random.Range(-spreadAngle/2, spreadAngle/2);
            float currentYAngle = Random.Range(-spreadAngle/2, spreadAngle/2);
                
            //cam.transform.Rotate(-rAngle,0,0);

            // Rotate direction by elevation (recoil)
            Quaternion elevation = Quaternion.AngleAxis(-rAngle, ShootPoint.right);
            Vector3 spreadDirection = Quaternion.Euler(currentYAngle, currentAngle, 0) * ShootPoint.forward;
            
            spreadDirection = elevation * spreadDirection;

            rb.velocity = spreadDirection * bulletSpeed;

            Debug.DrawRay(ShootPoint.position, spreadDirection * 10f, Color.green, 2f);
        }

        rAngle += recoil;

        // Assign damage to bullet
        ContactDamage bulletScript = bullet.GetComponent<ContactDamage>();
        if (bulletScript != null)
        {
            bulletScript.SetDamage(weaponDamage);
        }
        if (muzzleFlash != null)
        {   
            muzzleFlash.Play(); 
        }
        if (shootAudio != null)
        {
            shootAudio.Play(); 
        }



        Destroy(bullet, 2f);
    }


}
