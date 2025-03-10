using UnityEngine;
using UnityEngine.AI;

public class EnemyFSM : MonoBehaviour
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

    private void Awake()
    {
        agent = GetComponentInParent<NavMeshAgent>();

        if (agent == null)
        {
            print("[EnemyFSM] NavMeshAgent is missing on parent!");
        }
        
        if (sightSensor == null)
        {
            print("[EnemyFSM] Sight script is missing on AI object!");
        }

        agent.updateRotation = false;
    }

    void Update()
    {
        print("Current state: " + currentState);
        if (currentState == EnemyState.Idle) { Idle(); }
        else if (currentState == EnemyState.ChasePlayer) { ChasePlayer(); }
        else { AttackPlayer(); }
    }

    void Idle()
    {
        agent.isStopped = true;

        // If the enemy sees the player, start chasing
        if (sightSensor.detectedObject != null)
        {
            currentState = EnemyState.ChasePlayer;
        }
    }

    void ChasePlayer()
    { 
        agent.isStopped = false;
        agent.ResetPath();

        if (sightSensor.detectedObject == null)
        {
            currentState = EnemyState.Idle; // Stop moving if player is lost
            return;
        }

        // Move towards the player
        agent.SetDestination(sightSensor.detectedObject.transform.position);

        LookTo(sightSensor.detectedObject.transform.position);

        float distanceToPlayer = Vector3.Distance(transform.position, sightSensor.detectedObject.transform.position);

        if (distanceToPlayer < playerAttackDistance)
        {
            currentState = EnemyState.AttackPlayer;
        }
    }

    void AttackPlayer()
    {
        agent.isStopped = true;

        if (sightSensor.detectedObject == null)
        {
            currentState = EnemyState.Idle;
            return;
        } 

        LookTo(sightSensor.detectedObject.transform.position);
        Shoot();

        float distanceToPlayer = Vector3.Distance(transform.position, sightSensor.detectedObject.transform.position);

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

            GameObject bullet = Instantiate(bulletPrefab, shootPoint.transform.position, shootPoint.transform.rotation);

            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = shootPoint.forward * bulletSpeed;
            }

            Destroy(bullet, 2f);
        }
    }

    void LookTo(Vector3 targetPosition)
    {
        Vector3 directionToPosition = Vector3.Normalize(targetPosition - transform.parent.position);
        transform.parent.forward = directionToPosition;
    }

    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, playerAttackDistance);
    }
}
