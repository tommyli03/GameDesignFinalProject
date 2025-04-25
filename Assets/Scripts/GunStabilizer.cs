using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Members: Eric, Lucas, Thomas
 * Summary: Overrides the local rotation of a gun or weapon object to keep it visually stable during animations.
 * Useful for correcting unwanted weapon twist caused by character animation or bone rotation.
 */
public class GunStabilizer : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 localRotationOffset;

    void LateUpdate()
    {
        transform.localRotation = Quaternion.Euler(localRotationOffset);
    }
}
