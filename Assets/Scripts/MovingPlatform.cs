using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Vector3 pointA;
    public Vector3 pointB;
    public float speed = 5f;

    private bool movingToB = true;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movingToB ? pointB : pointA, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, movingToB ? pointB : pointA) < 0.1f)
        {
            movingToB = !movingToB;
        }
    }
}
