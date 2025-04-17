using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Summary: Handles dynamic camera zoom effects during gameplay actions like dashing.
 * This script increases the camera's field of view (FOV) briefly during a dash for visual emphasis,
 * then smoothly returns to the original FOV. It pulls dash duration from the Movement component.
 */

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

    // Initialize variables and configure zoom based on Movement component's dash duration
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

    // Continuously adjust FOV to create a smooth zoom effect during active zoom window
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

    // Triggers the zoom effect with a custom duration and FOV change intensity
    public void Zoom(float duration, float intensity)
    {
        endTime = Time.time + duration * .75f; 
        fovChange = intensity;
    }
}
