using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatAndSpin : MonoBehaviour
{
    
    public float floatAmplitude = 0.25f;
    public float floatSpeed = 2f;
    public float rotationSpeed = 50f;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        // Floating
        float newY = startPos.y + Mathf.Sin(Time.time * floatSpeed) * floatAmplitude;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);

        // Rotating
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.World);
    }
}
