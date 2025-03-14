using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 17f;
    public float mouseSensitivity = 2f;
    public Transform firstPersonCamera; 

    private Rigidbody rb;
    private float rotationX = 0f;

    private Grappling grapple;


    public bool freeze;
    public bool activeGrapple;


    private float dashTimer;
    public float dashCool = 1f;
    public float dashDuration = .6f;
    public float dashSpeed = 50f;
    private int jumps;
    public int jumpmax = 5;

    public CameraZoom cameraZoom;

    private float dt;

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

        if (Input.GetKeyDown(KeyCode.Space) && jumps > 0)
        {
            rb.velocity = new Vector3(rb.velocity.x, 15f, rb.velocity.z);
            jumps--;
        }
        
        if (Input.GetKeyDown(KeyCode.LeftShift) && dashTimer <= 0)
        {
            Vector3 dashDirection = transform.forward.normalized;

            rb.velocity = Vector3.zero;
            if (cameraZoom != null)
            {
                cameraZoom.Zoom(cameraZoom.zoomDuration, 12f);
            }
            StartCoroutine(Dash());
            //rb.AddForce(dashDirection * 1000f, ForceMode.VelocityChange);
            
            dashTimer = dashCool;

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


    }



    System.Collections.IEnumerator Dash()
    {
        float startTime = Time.time;

        // Get the dash direction based on the character's forward direction
        Vector3 dashDirection = transform.forward;

        while (Time.time < startTime + dashDuration)
        {
            // Calculate the dash progress (0 to 1)
            float dashProgress = (Time.time - startTime) / dashDuration;

            // Apply a smooth force (e.g., using a curve or easing function)
            float dashForce = Mathf.Lerp(dashSpeed, 0f, dashProgress);
            rb.velocity = dashDirection * dashForce;

            yield return null; // Wait for the next frame
        }

        // End the dash
        //rb.velocity = Vector3.zero; // Stop the dash movement
    }

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
