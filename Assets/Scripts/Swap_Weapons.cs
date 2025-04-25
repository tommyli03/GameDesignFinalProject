using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Members: Eric, Lucas, Thomas
 * Summary: Handles switching between multiple player weapons using the mouse scroll wheel
 * or number keys (1–9). Only one weapon is active at a time. Wraps around when scrolling past the ends.
 */
public class Swap_Weapons : MonoBehaviour
{
    public GameObject[] weapons;
    public int currentWeaponIndex = 0;

    void Start()
    {
        // At game start, activate the first weapon and deactivate the rest
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

        // Number key input 
        for (int i = 0; i < weapons.Length && i < 9; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                SwitchWeaponTo(i);
            }
        }
    }

    // Switches weapon based on scroll direction (+1 or -1), with wraparound logic
    void SwitchWeaponByOffset(int offset)
    {
        currentWeaponIndex = (currentWeaponIndex + offset + weapons.Length) % weapons.Length;
        UpdateWeapon();
    }

    // Switches directly to weapon at the given index (if valid)
    void SwitchWeaponTo(int index)
    {
        if (index < 0 || index >= weapons.Length || index == currentWeaponIndex)
            return;

        currentWeaponIndex = index;
        UpdateWeapon();
    }

    // Activates the selected weapon and deactivates all others
    void UpdateWeapon()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].SetActive(i == currentWeaponIndex);
        }
    }
}
