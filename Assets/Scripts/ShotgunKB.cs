using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunKB : MonoBehaviour
{
    // Start is called before the first frame update

    public bool active;

    public Camera cam;

    public bool collided = false;

    public float upkb;


    public void Fire()
    {
        active = true;
        Invoke(nameof(Deactivate), .1f);
    }

    void Deactivate()
    {
        active = false;
    }

    // Update is called once per frame
    // void OnTriggerEnter(Collider other)
    // {
    //     collided = true;
    //     Invoke(nameof(D2), .1f);
    //     if (active)
    //     {
    //         Vector3 kb = (other.GetComponentInParent<Transform>().position - cam.transform.position).normalized;

    //         other.GetComponentInParent<Rigidbody>().AddForce(kb * 20f, ForceMode.Impulse);
    //     }
        
    // }


    void OnTriggerStay(Collider other)
    {
        if (active && other.CompareTag("Enemy")) // Always tag enemies!
        {
            Vector3 kb = (other.transform.position - cam.transform.position).normalized;
            Rigidbody enemyRb = other.GetComponentInParent<Rigidbody>();
            
            if (enemyRb != null)
            {
                enemyRb.AddForce(other.transform.up * upkb, ForceMode.Impulse);
                enemyRb.AddForce(kb * 10f, ForceMode.Impulse);
            }
        }
    }

    void D2()
    {
        collided = false;
    }
}
