using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Members: Eric, Lucas, Thomas
 * Summary: Handles single-shot or automatic weapon shooting mechanics.
 * Simulates recoil, spread, bullet velocity, visual and audio effects.
 * Shoots a ray to determine the bullet's target, applies spread in both X/Y axes,
 * and instantiates a physical projectile.
 */
public class SniperShooting : MonoBehaviour
{
    public ParticleSystem muzzleFlash;
    public Camera playerCamera;
    public GameObject Bullet;
    public Transform ShootPoint;
    public float bulletSpeed;
    public float fireRate;
    private float lastShootTime;
    public float weaponDamage;
    public float recoil;
    public Camera cam;
    public float rAngle = 0f; 
    public AudioSource shootAudio;

    public GameObject gun;



    [Header("Aiming Settings")]
    [SerializeField] public float adsFOV = 20f; // Zoomed-in FOV
    [SerializeField] private float normalFOV = 80f; // Default FOV
    [SerializeField] private float adsTransitionSpeed = 12f;
    [SerializeField] public Transform adsGunPosition; // Assign in Inspector (empty GameObject at desired ADS position)
    [SerializeField] public Transform normalGunPosition; // Assign (default gun position)
    [SerializeField] public float adsRecoilMultiplier = 0.7f; // Reduce recoil when aiming


    private float currentRecoil = 1f;
    

    public bool isAiming = false;

    public float aimTime = 0f;


    void Start()
    {
        normalGunPosition.position = gun.transform.position;
    }

    void Update()
    {
        HandleAiming();
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
            rAngle *= .9f;
        }
        else 
            rAngle = 0f;

    }

    void HandleAiming()
    {
        // Toggle ADS on right mouse button
        bool aimInput = Input.GetMouseButton(1);
        
        if (aimInput != isAiming)
        {
            isAiming = aimInput;
            if (isAiming) 
                OnAimStart();
            else 
                OnAimEnd();
        }

        // Smoothly transition camera FOV
        cam.fieldOfView = Mathf.Lerp(
            cam.fieldOfView, 
            isAiming ? adsFOV : normalFOV, 
            adsTransitionSpeed * Time.deltaTime
        );

        // Smoothly move gun to ADS position
        gun.transform.position = Vector3.Lerp(
            gun.transform.position,
            isAiming ? adsGunPosition.position : normalGunPosition.position,
            adsTransitionSpeed * Time.deltaTime
        );

        if (aimTime > 0f)
            aimTime -= Time.deltaTime;
    }

    void OnAimStart()
    {
        currentRecoil = adsRecoilMultiplier;
        aimTime = .9f;
    }
    void OnAimEnd()
    {
        currentRecoil = 1f;
        aimTime = 0f;
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

        // Rotate direction by elevation (recoil)
        Quaternion elevation = Quaternion.AngleAxis(-rAngle * currentRecoil, ShootPoint.right);      
        Vector3 spreadDirection = elevation * ShootPoint.forward;

        // Add velocity to bullet
        GameObject bullet = Instantiate(Bullet, ShootPoint.position, Quaternion.LookRotation(spreadDirection));
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.velocity = bullet.transform.forward * bulletSpeed;

        //Debug.DrawRay(ShootPoint.position, spreadDirection * 10f, Color.green, 2f);
        
        rAngle += recoil;

        // Assign damage to bullet
        ContactDamage bulletScript = bullet.GetComponent<ContactDamage>();
        if (bulletScript != null)
        {
            bulletScript.SetDamage(weaponDamage);
        }
        if (muzzleFlash != null && !isAiming) //muzzle flash is right in front of camera when aiming
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

