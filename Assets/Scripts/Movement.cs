using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

/**
 * Members: Eric, Lucas, Thomas
 * Summary: Controls player movement including walking, jumping, dashing, and grappling interaction.
 * Handles camera rotation, sound feedback, screen effects, and physics-based traversal mechanics.
 * Includes support for moving platforms and dynamic camera zoom during special moves.
 */
public class Movement : MonoBehaviour
{
    public float moveSpeed = 17f;
    public float mouseSensitivity = 2f;
    public Transform firstPersonCamera; 
    public Rigidbody rb;
    private float rotationX = 0f;
    private Grappling grapple;
    public bool freeze;
    public bool activeGrapple;
    private float dashTimer;
    public float dashCool;
    public float dashDuration = .6f;
    public float dashSpeed = 50f;
    public int stamina = 1; // 1 = ready to dash, 2 = currently dashing, 3 = recharging dash
    private int jumps;
    public int jumpmax = 5;
    public CameraZoom cameraZoom;
    private float dt;
    public Volume dashVolume;
    public float volFade = 5f;
    private float targetVolumeWeight = 0f;
    public AudioSource footstepAudio;
    private Vector3 lastPosition;
    public AudioSource dashAudioSource;          


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        grapple = GetComponent<Grappling>();
        cameraZoom = GetComponent<CameraZoom>();
        Cursor.lockState = CursorLockMode.Locked; 
        Cursor.visible = false; 
        dashTimer = 0;
        jumps = jumpmax;
        //Physics.gravity = new Vector3(0, -19.62f, 0);
        dt = Time.deltaTime;
    }

    void Update()
    {
        // if (stamina == 1) { Debug.Log("Ready to dash!"); }
        if (stamina == 2) { Debug.Log("Dashing!"); }
        if (stamina == 3) { Debug.Log("Recharging!"); }
        
        // Mouse Look
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        rotationX -= mouseY; 
        rotationX = Mathf.Clamp(rotationX, -90f, 90f); 

        firstPersonCamera.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);

        if (activeGrapple)
            return;

        // Movement
        float moveX = Input.GetAxis("Horizontal"); 
        float moveZ = Input.GetAxis("Vertical");   

        Vector3 moveDirection = transform.right * moveX + transform.forward * moveZ;
        moveDirection.y = 0f; 


        rb.velocity = new Vector3(moveDirection.x * moveSpeed, rb.velocity.y, moveDirection.z * moveSpeed);
        if (dashTimer > 0)
            dashTimer -= dt;

        // Jumping
        if (Input.GetKeyDown(KeyCode.Space) && jumps > 0)
        {
            rb.velocity = new Vector3(rb.velocity.x, 15f, rb.velocity.z);
            jumps--;
        }

        // Dashing
        if (Input.GetKeyDown(KeyCode.LeftShift) && dashTimer <= 0 && stamina == 1 && stamina != 2 && stamina != 3)
        {
            Vector3 dashDirection = transform.forward.normalized;

            rb.velocity = Vector3.zero;

            if (cameraZoom != null)
            {
                cameraZoom.Zoom(cameraZoom.zoomDuration, 12f);
            }
            StartCoroutine(Dash());
            //rb.AddForce(dashDirection * 1000f, ForceMode.VelocityChange);

            stamina = 2;
            dashTimer = dashDuration; // Start dash duration
        }

        // Handle stamina-based dash/cooldown timing
        if (stamina == 2) // Dash is happening
        {
            dashTimer -= Time.deltaTime;
            if (dashTimer <= 0f)
            {
                stamina = 3;
                dashTimer = dashCool; // Ready to start cooldown
            }
        } 
        else if (stamina == 3) // Cooldown is happening
        {
            dashTimer -= Time.deltaTime;
            if (dashTimer <= 0f)
            {
                stamina = 1; // Ready to dash again
            }
        }

        // Post-Processing Volume Fade
        if (dashVolume != null)
        {
            dashVolume.weight = Mathf.Lerp(dashVolume.weight, targetVolumeWeight, volFade * Time.deltaTime);
        }

        RaycastHit hit;
        float rayLength = 1.1f; // Adjust based on your character's size
        if (Physics.Raycast(transform.position, Vector3.down, out hit, rayLength))
            jumps = jumpmax;
        
        if (freeze)
            rb.velocity = Vector3.zero;

        /*if (grapple.gcdTimer < 0)
            activeGrapple = false;
        if (activeGrapple)
            rb.velocity = Vector3.zero;*/

        // FOOTSTEP SOUND — Movement based on actual displacement
        bool isGrounded = IsGrounded();
        float distanceMoved = Vector3.Distance(transform.position, lastPosition);
        bool isActuallyMoving = distanceMoved > 0.01f;

        if (isActuallyMoving && isGrounded)
        {
            if (!footstepAudio.isPlaying)
            {
                footstepAudio.Play();
            }
        }
        else
        {
            if (footstepAudio.isPlaying)
            {
                footstepAudio.Stop();
            }
        }

        lastPosition = transform.position;

    }

    // Dash Coroutine
    System.Collections.IEnumerator Dash()
    {
        float startTime = Time.time;
        targetVolumeWeight = 1f;

        if (dashAudioSource != null)
        {
            dashAudioSource.Play();
        }

        // Get the dash direction based on the character's forward direction
        Vector3 dashDirection = transform.forward;

        while (Time.time < startTime + dashDuration)
        {
            // Calculate the dash progress (0 to 1)
            float dashProgress = (Time.time - startTime) / dashDuration;

            // Apply a smooth force (e.g., using a curve or easing function)
            float dashForce = Mathf.Lerp(dashSpeed, 0f, dashProgress);
            rb.velocity = dashDirection * dashForce;

            yield return null; 
        }

        targetVolumeWeight = 0f;

        // End the dash
        //rb.velocity = Vector3.zero; // Stop the dash movement
    }

    // Handles Grapple Movement
    public void JumpToPosition(Vector3 targetPos, float trajectoryHeight)
    {
        activeGrapple = true;
        targetV = GetJumpVelo(transform.position, targetPos, trajectoryHeight);
        Invoke(nameof(SetVelo), 0.1f);
        if (cameraZoom != null)
        {
            cameraZoom.Zoom(1.3f, 30f);
        }
        jumps += 1;
    }

    private Vector3 targetV;

    private void SetVelo()
    {
        rb.velocity = targetV;
    }

    // Calculates the velocity required to jump to a target position in an arc
    public Vector3 GetJumpVelo(Vector3 start, Vector3 end, float trajectoryHeight)
    {
        float gravity = Physics.gravity.y;
        float dispY = end.y - start.y;
        Vector3 dispXZ = new Vector3(end.x-start.x, 0f, end.z-start.z);

        Vector3 velY = Vector3.up * Mathf.Sqrt(-2 * gravity * trajectoryHeight);
        Vector3 velXZ = dispXZ / (Mathf.Sqrt(-2 * trajectoryHeight / gravity) + Mathf.Sqrt(2 * (dispY - trajectoryHeight) / gravity));

        return velXZ + velY;
    }

    //For player staying on moving platform
    void OnTriggerEnter(Collider other) {
        jumps = jumpmax;
        
        if (other.CompareTag("MovingPlatform")) {
            transform.parent = other.transform;
        }
    }

    void OnCollisionEnter(Collision other) {
        jumps = jumpmax;
    }

    //For player leaving moving platform
    void OnTriggerExit(Collider other) {
        if (other.CompareTag("MovingPlatform")) {
            transform.parent = null;
        }
    }

    //Checks if player is on the ground
    public bool IsGrounded()
    {
        RaycastHit hit;
        float rayLength = 1.1f; // Adjust based on your character's size
        if (Physics.Raycast(transform.position, Vector3.down, out hit, rayLength))
        {
            return true;
        }
        return false;
    }
    
}
