using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (Time.time >= lastShootTime + (1f / fireRate)) {
                Shoot();
                lastShootTime = Time.time;
            }
        }
    }

    void Shoot()
    {
        for (int i = 0; i < pelletCount; i++)
        {
            GameObject bullet = Instantiate(Bullet, ShootPoint.position, ShootPoint.rotation);
            
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
            {

                float currentAngle = Random.Range(-spreadAngle/2, spreadAngle/2);
                float currentYAngle = Random.Range(-spreadAngle/2, spreadAngle/2);
                //float angleStep = spreadAngle / (pelletCount - 1);
                //float currentAngle = -spreadAngle / 2 + (angleStep * i);
                
                Vector3 spreadDirection = Quaternion.Euler(currentYAngle, currentAngle, 0) * ShootPoint.forward;
                rb.velocity = spreadDirection * bulletSpeed;
            }
            
            Vector3 kb = cam.transform.forward.normalized;
            move.rb.AddForce(kb * -5f, ForceMode.Impulse);
            

            ContactDamage bulletScript = bullet.GetComponent<ContactDamage>();
            if (bulletScript != null)
            {
                bulletScript.SetDamage(weaponDamage);
            }

            Destroy(bullet, 0.5f); // RANGE
        }
    }
}
