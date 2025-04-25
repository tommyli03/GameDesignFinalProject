using UnityEngine;
using System;

/**
 * Members: Eric, Lucas, Thomas
 * Summary: Provides vision detection for AI using a field of view and line-of-sight check.
 * Detects valid objects within a radius and view cone, ignoring obstructed targets.
 * Useful for enemy AI behavior like chasing or targeting based on player visibility.
 */
public class Sight : MonoBehaviour
{
    public float distance;
    public float angle;
    public LayerMask objectsLayers;
    public LayerMask obstaclesLayers;

    public Collider detectedObject;

    void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(
            transform.position, distance, objectsLayers);
        
        detectedObject = null;
        for (int i = 0; i < colliders.Length; i++)
        {
            Collider collider = colliders[i];
            Vector3 directionToController = Vector3.Normalize(
                collider.bounds.center - transform.position);
            float angleToCollider = Vector3.Angle(
                transform.forward, directionToController);

            if (angleToCollider < angle)
            {
                if (!Physics.Linecast(transform.position, 
                        collider.bounds.center, out RaycastHit hit, obstaclesLayers))
                {
                    Debug.DrawLine(transform.position, 
                        collider.bounds.center, Color.green);
                    detectedObject = collider;
                    break;
                } else {
                    Debug.DrawLine(transform.position, hit.point, Color.red);
                }
            }
        }
    }

    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, distance);

        Vector3 rightDirection = Quaternion.Euler(0, angle, 0) * transform.forward;
        Gizmos.DrawRay(transform.position, rightDirection * distance);
        
        Vector3 leftDirection = Quaternion.Euler(0, -angle, 0) * transform.forward;
        Gizmos.DrawRay(transform.position, leftDirection * distance);
    }
}
