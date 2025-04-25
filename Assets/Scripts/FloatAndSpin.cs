using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Members: Eric, Lucas, Thomas
 * Summary: Applies a smooth floating and spinning effect to a GameObject (right now just the health pack).
 * Used for making pickups or objects visually stand out with motion.
 * Float motion is sinusoidal; rotation is continuous around the Y-axis.
 */
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
