using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{


    private float zoomFOV = 100f; // Field of view during zoom
    public float zoomDuration = 0.6f; // Duration of the zoom
    public float fovChange = 25f;
    public float distort = 10f;

    private float originalFOV;
    public Camera cam;
    private float endTime;

    private float distortEnd;


    private Movement move;



    // Start is called before the first frame update
    void Start()
    {
        move = GetComponent<Movement>();
        //cam = GetComponent<Camera>();
        zoomDuration = move.dashDuration * .8f;
        originalFOV = cam.fieldOfView;
        zoomFOV = originalFOV + fovChange;
        endTime = Time.time - 1f;

        distortEnd = Time.time -1f;
    }

    // Update is called once per frame
    void Update()
    {   
        if (Time.time < endTime)
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, zoomFOV, Time.deltaTime * 10f);
        else
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, originalFOV, Time.deltaTime * 10f);

        // if (Time.time < distortEnd)
        //     cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, zoomFOV, Time.deltaTime * 10f);
        // else
        //     cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, originalFOV, Time.deltaTime * 10f);


    }

    public void Zoom(float duration, float intensity)
    {
        endTime = Time.time + duration * .75f;
        fovChange = intensity;
    }
}
