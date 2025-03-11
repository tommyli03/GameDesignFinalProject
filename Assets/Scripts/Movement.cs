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
                cameraZoom.Zoom();
            }
            StartCoroutine(Dash());
            //rb.AddForce(dashDirection * 1000f, ForceMode.VelocityChange);
            
            dashTimer = dashCool;

        }

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


    public void JumpToPos(Vector3 target)
    {
        activeGrapple = true;
        
        rb.position = target;


    }

    //For player staying on moving platform
    void OnTriggerEnter(Collider other) {
        jumps = jumpmax;
        

        if (other.CompareTag("MovingPlatform")) {
            transform.parent = other.transform;
        }
    }

    //For player leaving moving platform
    void OnTriggerExit(Collider other) {
        if (other.CompareTag("MovingPlatform")) {
            transform.parent = null;
        }
    }
}
