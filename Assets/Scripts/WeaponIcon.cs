using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Members: Eric, Lucas, Thomas
 * Summary: Displays the currently equipped weapon icon on the screen based on the player's selected weapon.
 * Pulls icon data from an array and positions the texture at the top-right corner using screen-relative sizing.
 * Continuously tracks the weapon index from the Swap_Weapons script.
 */
public class WeaponIcon : MonoBehaviour
{
    public GameObject playerObj;
    public Texture2D[] weaponIcons;         // Assign your icons in the correct index order
    private Vector2 relativeIconSize = new Vector2(0.175f, 0.25f); // e.g., 17.5% width, 25% height
    private Vector2 relativePadding = new Vector2(0.03f, 0.03f);  // e.g., 3% padding from top-right

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

        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        // Scale icon size and padding relative to screen dimensions
        Vector2 iconSize = new Vector2(screenWidth * relativeIconSize.x, screenHeight * relativeIconSize.y);
        Vector2 padding = new Vector2(screenWidth * relativePadding.x, screenHeight * relativePadding.y);

        float x = screenWidth - iconSize.x - padding.x;
        float y = padding.y;

        GUI.DrawTexture(new Rect(x, y, iconSize.x, iconSize.y), icon);
    }
}
