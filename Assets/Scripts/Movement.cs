using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float mouseSensitivity = 2f;
    public Transform firstPersonCamera; 

    private Rigidbody rb;
    private float rotationX = 0f;

    private Grappling grapple;


    public bool freeze;
    public bool activeGrapple;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        grapple = GetComponent<Grappling>();
        Cursor.lockState = CursorLockMode.Locked; 
        Cursor.visible = false; 
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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector3(rb.velocity.x, 5f, rb.velocity.z);
        }
        
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            rb.AddForce(transform.forward * 100f, ForceMode.Impulse);
        }

        if (freeze)
            rb.velocity = Vector3.zero;

        if (grapple.gcdTimer < 0)
            activeGrapple = false;
        if (activeGrapple)
            rb.velocity = Vector3.zero;


    }


    public void JumpToPos(Vector3 target)
    {
        activeGrapple = true;
        
        rb.position = target;


    }

    //For player staying on moving platform
    void OnTriggerEnter(Collider other) {
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
