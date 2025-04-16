using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swap_Weapons : MonoBehaviour
{
    public GameObject[] weapons;
    public int currentWeaponIndex = 0;

    void Start()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].SetActive(i == 0);
        }
        currentWeaponIndex = 0;
    }

    void Update()
    {
        // Scroll wheel input
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0f)
        {
            SwitchWeaponByOffset(1);
        }
        else if (scroll < 0f)
        {
            SwitchWeaponByOffset(-1);
        }

        // Number key input (Alpha1 = weapon 0, Alpha2 = weapon 1, etc.)
        for (int i = 0; i < weapons.Length && i < 9; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                SwitchWeaponTo(i);
            }
        }
    }

    void SwitchWeaponByOffset(int offset)
    {
        currentWeaponIndex = (currentWeaponIndex + offset + weapons.Length) % weapons.Length;
        UpdateWeapon();
    }

    void SwitchWeaponTo(int index)
    {
        if (index < 0 || index >= weapons.Length || index == currentWeaponIndex)
            return;

        currentWeaponIndex = index;
        UpdateWeapon();
    }

    void UpdateWeapon()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].SetActive(i == currentWeaponIndex);
        }
    }
}
