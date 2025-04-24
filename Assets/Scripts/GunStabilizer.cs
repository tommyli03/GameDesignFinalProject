using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunStabilizer : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 localRotationOffset;

    void LateUpdate()
    {
        transform.localRotation = Quaternion.Euler(localRotationOffset);
    }
}
