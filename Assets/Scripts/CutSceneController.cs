using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Events;
using System.Collections.Generic;

/**
 * Members: Eric, Lucas, Thomas
 * Summary: Controls the behavior of an in-game cutscene using a Cinemachine dolly track and virtual camera.
 * Disables gameplay elements (enemies, weapons, UI) during the cutscene and re-enables them afterward.
 * Also manages player control scripts (e.g., grappling and weapon swapping) to ensure they're paused during the cutscene.
 */

public class CutSceneController : MonoBehaviour
{
    [SerializeField] private GameObject cutsceneMessage;
    [SerializeField] private GameObject gameUI;
    public GameObject vcamCutscene;                // The virtual camera GameObject
    public CinemachineDollyCart dollyCart;         // The dolly cart moving on the track
    private List<GameObject> weaponObjects = new List<GameObject>();
    public float cutsceneDuration = 10f;           // How long the cutscene should last
    public float cutsceneSpeed = 0.2f;             // How fast the dolly cart moves
    public Transform cameraAnchor;
    public GameObject firstPersonCamera;
    private GameObject[] cachedEnemies;
    private Grappling grapplingScript;
    private Swap_Weapons swapWeapons;
    private Movement movementScript;



    void Start()
    {
        // Disable all enemies
        // Cache enemies while they are still active
        cachedEnemies = GameObject.FindGameObjectsWithTag("Enemy");

        // Disable enemies
        foreach (var enemy in cachedEnemies)
        {
            enemy.SetActive(false);
        }
        // Cache all child weapons under FirstPersonCamera
        foreach (Transform child in firstPersonCamera.transform)
        {
            if (child.name == "Default" || child.name == "Shotgun" || child.name == "Sniper" || child.name.Contains("Plasma"))
            {
                weaponObjects.Add(child.gameObject);
                child.gameObject.SetActive(false); // Disable weapon
            }
        }

        if (gameUI != null) gameUI.SetActive(false);
        if (cutsceneMessage != null) cutsceneMessage.SetActive(true);

        if (!Application.isPlaying) return;
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            movementScript = player.GetComponent<Movement>();
            grapplingScript = player.GetComponent<Grappling>();
            swapWeapons = player.GetComponent<Swap_Weapons>();
            if (grapplingScript != null) grapplingScript.enabled = false;
            if (swapWeapons != null) swapWeapons.enabled = false;
            if (movementScript != null) movementScript.enabled = false;
        }



        // Enable the virtual camera
        if (vcamCutscene != null)
            vcamCutscene.SetActive(true);

        // ▶ Start dolly movement
        if (dollyCart != null)
        {
            dollyCart.m_Position = 0f;
            dollyCart.m_Speed = cutsceneSpeed;
        }

        // ⏱ Schedule end of cutscene
        Invoke(nameof(EndCutscene), cutsceneDuration);
    }

    void EndCutscene()
    {
        if (dollyCart != null) dollyCart.m_Speed = 0f;
        if (vcamCutscene != null) vcamCutscene.SetActive(false);
        if (grapplingScript != null) grapplingScript.enabled = true;
        if (swapWeapons != null) swapWeapons.enabled = true;
        if (movementScript != null) movementScript.enabled = true;


        // Re-enable all enemies
        foreach (var enemy in cachedEnemies)
        {
            if (enemy != null)
                enemy.SetActive(true);
        }

        foreach (GameObject weapon in weaponObjects)
        {
            if (weapon != null)
            {
                // Only re-enable the Default weapon
                if (weapon.name == "Default" || weapon.name.Contains("Plasma"))
                    weapon.SetActive(true);
            }
        }

        if (gameUI != null) gameUI.SetActive(true);
        if (cutsceneMessage != null) cutsceneMessage.SetActive(false);

        // Snap FirstPersonCamera back to player head
        if (firstPersonCamera != null && cameraAnchor != null)
        {
            firstPersonCamera.transform.SetPositionAndRotation(
                cameraAnchor.position,
                cameraAnchor.rotation
            );
        }
    }
}
