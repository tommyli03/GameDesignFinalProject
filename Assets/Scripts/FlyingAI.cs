using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;

public class FlyingAI : MonoBehaviour
{
    public enum EnemyState { Idle, ChasePlayer, AttackPlayer }
    public EnemyState currentState = EnemyState.Idle; // Start in Idle state

    public Sight sightSensor;
    public float playerAttackDistance;

    private NavMeshAgent agent;
    public float lastShootTime;
    public GameObject bulletPrefab;
    public float bulletSpeed;
    public float fireRate;
    public Transform shootPoint;

    public float damage;
    
    private Transform player; // Store player reference

    private void Awake()
    {
        agent = GetComponentInParent<NavMeshAgent>();

        if (agent == null)
        {
            Debug.LogError("[EnemyFSM] NavMeshAgent is missing on parent!");
        }
        
        if (sightSensor == null)
        {
            Debug.LogError("[EnemyFSM] Sight script is missing on AI object!");
        }

        agent.updateRotation = false; // We handle rotation manually
    }

    private void Start()
    {
        // Try to find the player automatically if not assigned
        if (sightSensor.detectedObject != null)
        {
            player = sightSensor.detectedObject.transform;
        }
    }

    void Update()
    {
        if (sightSensor.detectedObject != null)
        {
            player = sightSensor.detectedObject.transform;
        }

        // Debug.Log("Current state: " + currentState);
        if (currentState == EnemyState.Idle) Idle();
        else if (currentState == EnemyState.ChasePlayer) ChasePlayer();
        else AttackPlayer();
    }

    void Idle()
    {
        agent.isStopped = true;

        if (player != null)
        {
            currentState = EnemyState.ChasePlayer;
        }
    }

    void ChasePlayer()
    { 
        agent.isStopped = false;

        if (player == null)
        {
            currentState = EnemyState.Idle; // Stop moving if player is lost
            return;
        }

        // Move towards the player
        agent.SetDestination(player.position);

        // Face the player smoothly
        LookTo(player.position);

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer < playerAttackDistance)
        {
            currentState = EnemyState.AttackPlayer;
        }
    }

    void AttackPlayer()
    {
        agent.isStopped = true;

        if (player == null)
        {
            currentState = EnemyState.Idle;
            return;
        } 

        // Ensure the robot faces the player correctly
        LookTo(player.position);

        // Shoot at the player
        Shoot();

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer > playerAttackDistance * 1.1f)
        {
            currentState = EnemyState.ChasePlayer;
        }
    }

    void Shoot()
    {
        var timeSinceLastShoot = Time.time - lastShootTime;
        if (timeSinceLastShoot > fireRate)
        {
            lastShootTime = Time.time;

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

            Destroy(bullet, 2f);
        }
    }


    void LookTo(Vector3 targetPosition)
    {
        if (transform.parent != null)
        {
            Vector3 lookDirection = (targetPosition - transform.parent.position).normalized;
            lookDirection.y = 0; // Keep rotation flat to avoid tilting
            transform.parent.forward = Vector3.Lerp(transform.parent.forward, lookDirection, Time.deltaTime * 5f); // Smooth rotation
        }
    }

    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, playerAttackDistance);
    }
}
