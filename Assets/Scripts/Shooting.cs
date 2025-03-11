using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject Bullet;
    public Transform ShootPoint;
    public float bulletSpeed;
    public float fireRate;
    private float lastShootTime;
    public float weaponDamage;

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (Time.time >= lastShootTime + (1f / fireRate)) {
                Shoot();
                lastShootTime = Time.time;
            }
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(Bullet, ShootPoint.position, ShootPoint.rotation);
        
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = ShootPoint.forward * bulletSpeed;
        }

        ContactDamage bulletScript = bullet.GetComponent<ContactDamage>();
        if (bulletScript != null)
        {
            bulletScript.SetDamage(weaponDamage);
        }
        
        Destroy(bullet, 2f);
    }
}
