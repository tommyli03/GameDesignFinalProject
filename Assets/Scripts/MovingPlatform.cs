using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Summary: Moves a platform back and forth between two points (pointA and pointB) at a constant speed.
 * Designed to support moving platforms that players can ride or interact with in the game world.
 */
public class MovingPlatform : MonoBehaviour
{
    public Vector3 pointA;
    public Vector3 pointB;
    public float speed = 5f;
    private bool movingToB = true;

    void Update()
    {
        // Move the platform toward its current target point
        transform.position = Vector3.MoveTowards(transform.position, movingToB ? pointB : pointA, speed * Time.deltaTime);

        // Switch direction if platform is close enough to target
        if (Vector3.Distance(transform.position, movingToB ? pointB : pointA) < 0.1f)
        {
            movingToB = !movingToB;
        }
    }
}
