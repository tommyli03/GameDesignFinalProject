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
    public float spreadAngle;

    public float recoil;

    private float rAngle = 0f; //total recovery, 6-10f, 5 degrees

    
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (Time.time >= lastShootTime + (1f / fireRate)) {
                Shoot();
                lastShootTime = Time.time;
            }
        }
        if (rAngle < 0)
        {
            if (rAngle > -2f)
                rAngle += .5f;
            else
                rAngle *= .8f;
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(Bullet, ShootPoint.position, ShootPoint.rotation);
        
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            float currentAngle = Random.Range(-spreadAngle/2, spreadAngle/2);
            float currentYAngle = Random.Range(-spreadAngle/2, spreadAngle/2);
                
                
            Vector3 spreadDirection = Quaternion.Euler(currentYAngle + rAngle, currentAngle, 0) * ShootPoint.forward;
            rb.velocity = spreadDirection * bulletSpeed;
            
        }
        rAngle += recoil;

        ContactDamage bulletScript = bullet.GetComponent<ContactDamage>();
        if (bulletScript != null)
        {
            bulletScript.SetDamage(weaponDamage);
        }
        
        Destroy(bullet, 2f);
    }
}
