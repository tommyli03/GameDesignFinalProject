using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FlyingAI : MonoBehaviour
{
    public Sight sightSensor;
    public float playerAttackDistance;
    public GameObject bulletPrefab;
    public float bulletSpeed;
    public float fireRate;

    public float burstCooldown;
    public Transform shootPoint;
    public float damage;
    public float hoverHeight; 
    public float floatSpeed = 1f; // Speed of floating motion

    public float attackRange = 24f;

    private Transform player;
    private Animator animator;
    private bool alreadyShot;
    private float lastShootTime;


    private float originalY; 

    public LayerMask whatIsPlayer;

    private Rigidbody rb;

    private void Awake()
    {
        animator = GetComponentInParent<Animator>();
        rb = GetComponent<Rigidbody>(); // Get Rigidbody component
        rb.useGravity = false; // Disable gravity
        originalY = transform.position.y;

        // Disable NavMeshAgent if it exists
        
    }

    private void Start()
    {
        if (sightSensor.detectedObject != null)
        {
            player = sightSensor.detectedObject.transform;
        }
    }

    void Update()
    {
        Float();
        if (sightSensor.detectedObject == null)
        {
            Idle();
        }
        else 
        {
            
            player = sightSensor.detectedObject.transform;
            AttackPlayer();
        }
    }

    void Float()
    {
        // Hover at a fixed height with slight bobbing


        float newY = originalY + hoverHeight + Mathf.Sin(Time.time * floatSpeed) * 0.3f;
        float swayX = Mathf.Sin(Time.time * floatSpeed * 0.5f) * 0.1f;
        float swayZ = Mathf.Cos(Time.time * floatSpeed * 0.5f) * 0.1f;


        Vector3 target = new Vector3(transform.position.x + swayX, newY, transform.position.z + swayZ);

        rb.MovePosition(Vector3.Lerp(transform.position, target, Time.deltaTime * 5f));
    }

    void Idle()
    {
        // Just float in place when no player is detected
        LookTo(transform.position + transform.forward * 10f); // Look forward
    }

    void AttackPlayer()
    {
        // Always face the player
        LookTo(player.position);

        
        if (!alreadyShot)
        {
            Burst();
            alreadyShot = true;
            Invoke(nameof(ResetAttack), burstCooldown);
        }
    }

    void ResetAttack()
    {
        alreadyShot = false;
    }

    void Burst() //this one currently doesnt work as intended. everytime I try and change it and then run it, Unity straight up crashes and Im forced to revert it
    {
        var timeSinceLastShoot = Time.time - lastShootTime;
        for(int i=0; i<5; i++)
        {
            Invoke(nameof(Shoot), (i*fireRate+.05f));
        }
        
        
    }

    void Shoot()
    {

            if (player == null) return;

            // Calculate direction to player
            Vector3 directionToPlayer = (player.position - shootPoint.position).normalized;

            // Instantiate the bullet
            GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.LookRotation(directionToPlayer));

            // Apply force in the correct direction
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = directionToPlayer * bulletSpeed; // Ensure the bullet moves toward the player
            }

            ContactDamage bulletScript = bullet.GetComponent<ContactDamage>();
            if (bulletScript != null)
            {
                bulletScript.SetDamage(damage);
            }

            Destroy(bullet, 1f);
    }

    void LookTo(Vector3 targetPosition)
    {
        if (transform.parent != null)
        {
            Vector3 lookDirection = (targetPosition - transform.parent.position).normalized;
            lookDirection.y = 0; // Keep rotation flat
            transform.parent.forward = Vector3.Lerp(transform.parent.forward, lookDirection, Time.deltaTime * 5f);
        }
    }
}