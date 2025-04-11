using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public float rAngle = 0f; //total recovery, 6-10f, 5 degrees


    
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (Time.time >= lastShootTime + (1f / fireRate)) {
                Shoot();
                lastShootTime = Time.time;
            }
        }
        if (rAngle > 0f)
        {
            if (rAngle < 2f)
                rAngle -= .5f;
            rAngle *= .8f;
        }
    }
    void Shoot()
    {
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

        float spreadX = Random.Range(-spreadAngle / 2f, spreadAngle / 2f);
        float spreadY = Random.Range(-spreadAngle / 2f, spreadAngle / 2f);
        Quaternion spreadRotation = Quaternion.Euler(spreadY + rAngle, spreadX, 0);
        Vector3 finalDirection = spreadRotation * shootDirection;

        GameObject bullet = Instantiate(Bullet, ShootPoint.position, Quaternion.LookRotation(finalDirection));

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            float currentAngle = Random.Range(-spreadAngle/2, spreadAngle/2);
            float currentYAngle = Random.Range(-spreadAngle/2, spreadAngle/2);
                
            //cam.transform.Rotate(-rAngle,0,0);
            Quaternion elevation = Quaternion.AngleAxis(-rAngle, ShootPoint.right);
            Vector3 spreadDirection = Quaternion.Euler(currentYAngle, currentAngle, 0) * ShootPoint.forward;
            
            spreadDirection = elevation * spreadDirection;

            rb.velocity = spreadDirection * bulletSpeed;

            Debug.DrawRay(ShootPoint.position, spreadDirection * 10f, Color.green, 2f);
            
        }

        rAngle += recoil;

        ContactDamage bulletScript = bullet.GetComponent<ContactDamage>();
        if (bulletScript != null)
        {
            bulletScript.SetDamage(weaponDamage);
        }
        if (muzzleFlash != null)
        {
            // Debug.Log("Playing muzzle flash");
            
            muzzleFlash.Play(); // replays the burst cleanly

        }


        Destroy(bullet, 2f);
    }


    // void Shoot()
    // {
    //     GameObject bullet = Instantiate(Bullet, ShootPoint.position, ShootPoint.rotation);
        
    //     Rigidbody rb = bullet.GetComponent<Rigidbody>();
    //     if (rb != null)
    //     {
    //         float currentAngle = Random.Range(-spreadAngle/2, spreadAngle/2);
    //         float currentYAngle = Random.Range(-spreadAngle/2, spreadAngle/2);
                
                
    //         Vector3 spreadDirection = Quaternion.Euler(currentYAngle + rAngle, currentAngle, 0) * ShootPoint.forward;
    //         rb.velocity = spreadDirection * bulletSpeed;
            
    //     }
    //     rAngle += recoil;

    //     ContactDamage bulletScript = bullet.GetComponent<ContactDamage>();
    //     if (bulletScript != null)
    //     {
    //         bulletScript.SetDamage(weaponDamage);
    //     }
        
    //     Destroy(bullet, 2f);
    // }
}
