using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grappling : MonoBehaviour
{

 
    private Movement pm;
    public Transform cam;
    public Transform gunTip;
    public LayerMask Grabbable;
    public LineRenderer lr;

    public float maxGrappleDist;
    public float grappleDelayTime;


    private Vector3 grapplePoint; //change to private later
    public float grapplingCool; //cooldown
    public float gcdTimer = 0;

    public KeyCode grappleKey = KeyCode.Mouse1;
    public bool grappling;


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
        if(grappling)
            lr.SetPosition(0, gunTip.position);
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
        if (Physics.Raycast(cam.position, cam.forward, out hit, maxGrappleDist, Grabbable)) //no idea what this does, try and figure out.
        {
            grapplePoint = hit.point;

            Invoke(nameof(Execute), grappleDelayTime);
        }
        else
        {
            grapplePoint = cam.position + cam.forward*maxGrappleDist;

            Invoke(nameof(StopGrapple), grappleDelayTime);
        }


        lr.enabled = true;
        lr.SetPosition(1, grapplePoint);
    }

    private void Execute()
    {
        pm.freeze = false;
        pm.JumpToPos(grapplePoint);
    }

    private void StopGrapple()
    {
        grappling = false;

        gcdTimer = grapplingCool;

        lr.enabled = false;

        pm.freeze = false;
    }
}
