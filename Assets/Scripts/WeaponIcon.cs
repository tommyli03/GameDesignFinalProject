using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponIcon : MonoBehaviour
{
    public GameObject playerObj;
    public Texture2D[] weaponIcons;         // Assign your icons in the correct index order
    public Vector2 iconSize = new Vector2(64, 64); // Size of the icon box
    public Vector2 padding = new Vector2(20, 20);   // Distance from top-right corner

    private int currentWeaponIndex = 0;
    private Swap_Weapons swapWeaponsScript;
    
    // Start is called before the first frame update
    void Start()
    {
        Swap_Weapons playerWeapons = playerObj.transform.Find("Player").GetComponent<Swap_Weapons>();

        // Find the Swap_Weapons script on the player
        if (playerWeapons != null)
        {
            currentWeaponIndex = playerWeapons.currentWeaponIndex;
        }

    }

    // Update is called once per frame
    void Update()
    {
        Swap_Weapons playerWeapons = playerObj.transform.Find("Player").GetComponent<Swap_Weapons>();

        if (playerWeapons != null)
        {
            currentWeaponIndex = playerWeapons.currentWeaponIndex;
        }
    }

    void OnGUI()
    {
        if (weaponIcons == null || weaponIcons.Length == 0) return;

        if (currentWeaponIndex < 0 || currentWeaponIndex >= weaponIcons.Length) return;

        Texture2D icon = weaponIcons[currentWeaponIndex];
        if (icon == null) return;

        float x = Screen.width - iconSize.x - padding.x;
        float y = padding.y;

        GUI.DrawTexture(new Rect(x, y, iconSize.x, iconSize.y), icon);
    }
}
