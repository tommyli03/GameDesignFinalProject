using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grappling : MonoBehaviour
{

 
    private Movement pm;
    public Transform cam;
    public Transform gunTip;
    public LayerMask Grabbable;
    // public LineRenderer lr;

    public GameObject laserBeamPrefab;  // Assign your laser asset in the Inspector
    private GameObject activeLaserBeam; // Stores the instantiated laser

    public float maxGrappleDist;
    public float grappleDelayTime;
    public float yOvershoot = 1f;


    private Vector3 grapplePoint; //change to private later
    public float grapplingCool; //cooldown
    public float gcdTimer = 0;

    public KeyCode grappleKey = KeyCode.Mouse1;
    public bool grappling;
    public bool activeGrapple;


    private float dt; 


    // Start is called before the first frame update
    void Start()
    {
        pm = GetComponent<Movement>();
        dt = Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(grappleKey))
        {
            StartGrapple();
        }

        if (gcdTimer > 0)
            gcdTimer -= dt;
    }


    void LateUpdate()
    {
        // if(grappling)
        //     lr.SetPosition(0, gunTip.position);

        if (grappling && activeLaserBeam != null)
        {
            UpdateLaser();
        }
    }

    private void UpdateLaser()
    {
        if (activeLaserBeam != null)
        {
            activeLaserBeam.transform.position = gunTip.position; // Start at the gun tip
            activeLaserBeam.transform.LookAt(grapplePoint); // Point toward grapple target

            // Adjust scale if needed (for stretched beam effects)
            float distance = Vector3.Distance(gunTip.position, grapplePoint);
            activeLaserBeam.transform.localScale = new Vector3(1, 1, distance);
        }
    }




    private void StartGrapple()
    {
        if (gcdTimer > 0)
        {
            return;
        }

        pm.freeze = true;
        grappling = true;
        RaycastHit hit;
        if (Physics.Raycast(cam.position, cam.forward, out hit, maxGrappleDist, Grabbable)) 
        {
            grapplePoint = hit.point;

            Invoke(nameof(Execute), grappleDelayTime);
        }
        else
        {
            grapplePoint = cam.position + cam.forward*maxGrappleDist;

            Invoke(nameof(StopGrapple), grappleDelayTime);
        }


        // lr.enabled = true;
        // lr.SetPosition(1, grapplePoint);
        if (laserBeamPrefab != null)
        {
            activeLaserBeam = Instantiate(laserBeamPrefab, gunTip.position, Quaternion.identity);
        }

        UpdateLaser();
    }

    

    private void Execute()
    {
        pm.freeze = false;

        Vector3 lowestPoint = new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z);

        float grapplePosRelY = grapplePoint.y - lowestPoint.y;
        float topArcPoint = grapplePosRelY + yOvershoot;

        if (grapplePosRelY < 0)
        {
            topArcPoint = yOvershoot;
        }


        pm.JumpToPosition(grapplePoint, topArcPoint);

        Invoke(nameof(StopGrapple), 1.3f);
    }

    private void StopGrapple()
    {
        Debug.Log("Stopping grapple");
        grappling = false;

        gcdTimer = grapplingCool;

        //lr.enabled = false;

        pm.freeze = false;
        pm.activeGrapple = false;

        if (activeLaserBeam != null)
        {
            Destroy(activeLaserBeam);
        }
    }
}
