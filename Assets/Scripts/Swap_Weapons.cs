using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swap_Weapons : MonoBehaviour
{
    public GameObject[] weapons;
    public int currentWeaponIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].SetActive(i == 0);
        }

        currentWeaponIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0) {
            SwitchWeapon(1);
        } else if (Input.GetAxis("Mouse ScrollWheel") < 0) {
            SwitchWeapon(-1);
        }
    }

    void SwitchWeapon(int direction) {
        currentWeaponIndex = (currentWeaponIndex + direction + weapons.Length) % weapons.Length;
        UpdateWeapon();
    }

    void UpdateWeapon() {
        for (int i = 0; i < weapons.Length; i++) {
            weapons[i].SetActive(i == currentWeaponIndex);
        }
    }
}
