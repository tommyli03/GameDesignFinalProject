using UnityEngine;
using UnityEngine.AI;

public class EnemyFSM : MonoBehaviour
{
   

    public Sight sightSensor;
    public float playerAttackDistance;

    private NavMeshAgent agent;
    public float lastShootTime;
    public GameObject bulletPrefab;
    public float bulletSpeed;
    public float fireRate;
    public Transform shootPoint;

    public LayerMask whatIsGround;
    public LayerMask whatIsPlayer;
    public float damage;
    
    //references
    private Transform player; // Store player reference
    private Animator animator;

    //Patrolling
    public Vector3 walkPoint;
    
    public bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float burstCooldown;
    bool alreadyShot; 


    //States
    public float stationaryAttackRange;
    public bool playerInAttackRange;

    public float distWalk;
    
    
    private void Awake() //sets everything up upon start
    {


        agent = GetComponentInParent<NavMeshAgent>();
        animator = GetComponentInParent<Animator>();

        NavMeshHit hit;
        if (NavMesh.SamplePosition(transform.position, out hit, 5f, NavMesh.AllAreas))
        {
            transform.position = hit.position;
        }
        if (agent == null)
        {
            Debug.LogError("[EnemyFSM] NavMeshAgent is missing on parent!");
        }
        
        if (sightSensor == null)
        {
            Debug.LogError("[EnemyFSM] Sight script is missing on AI object!");
        }

        agent.updateRotation = false; // We handle rotation manually 
        walkPointSet = false;
    }

    private void Start()
    {
        // Try to find the player automatically if not assigned
        if (sightSensor.detectedObject != null)
        {
            player = sightSensor.detectedObject.transform;
        }
    }

    void Update() //sets state by determining if theres a player, and where they are
    {
        if (sightSensor.detectedObject == null)
        {
            Idle();
            playerInAttackRange = false;
        }
        else 
        {
            agent.speed = 10f;
            agent.updateRotation = false;
            player = sightSensor.detectedObject.transform;
            playerInAttackRange = Physics.CheckSphere(transform.position, stationaryAttackRange, whatIsPlayer);
            if (!playerInAttackRange)
                ChasePlayer();
            else
                AttackPlayer();
        // Debug.Log("Current state: " + currentState);
        
        }
    }

    void Idle() //Idle is randomly walking around
    {
        agent.speed = 2f;
        agent.updateRotation = true;
        if (!walkPointSet)
        {
            SearchWalkPoint(); //grabs a place to walk to, walks there, and repeats
        }
        else
            agent.SetDestination(walkPoint);

        Vector3 distanceWalk = transform.position - walkPoint;
        distWalk = distanceWalk.magnitude;
        if (distanceWalk.magnitude < 2f)
        {
            Debug.LogError("Setting false!");
            //walkPointSet = false;
            Invoke(nameof(SearchWalkPoint), burstCooldown);
        }
        //agent.isStopped = true;

    }

    void SearchWalkPoint() //function for random point to walk to
    {
        Debug.LogError("Searching!");
        float walkx = Random.Range(-walkPointRange, walkPointRange);
        float walkz = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + walkx, transform.position.y, transform.position.z + walkz);
        walkPointSet = true;
        

        //old code that I cant find out how to debug. will come back and use if I figure it out
        //if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        //    walkPointSet = true;
        
    }

    void ChasePlayer() 
    { 
        agent.isStopped = false;
        animator.SetBool("isChasing", true);


        // Move towards the player
        agent.SetDestination(player.position);

        // Face the player smoothly
        LookTo(player.position);

        
    }

    void AttackPlayer()
    {
        agent.isStopped = true;
       

        // Ensure the robot faces the player correctly
        LookTo(player.position);

        // Shoot at the player
        if (!alreadyShot)
        {
            Burst();
            alreadyShot = true;
            Invoke(nameof(ResetAttack), burstCooldown); //shoots, and then resets the shooting restriction after a cooldown.

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
            lookDirection.y = 0; // Keep rotation flat to avoid tilting
            transform.parent.forward = Vector3.Lerp(transform.parent.forward, lookDirection, Time.deltaTime * 5f); // Smooth rotation
        }
    }

    void OnDrawGizmos() 
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, playerAttackDistance);
    }
}
