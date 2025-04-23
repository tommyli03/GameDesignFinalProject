using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stamina : MonoBehaviour
{
    public GameObject playerObj;
    public Texture2D tickTexture;
    private Vector2 relativeStartPosition = new Vector2(0.05f, 0.125f); // 5% from top-left
    private Vector2 relativeTickSize = new Vector2(0.0125f, 0.05f); // Tick size relative to screen
    private int totalTicks = 50;
    private int currentTicks;
    private bool isRecharging = false;
    private float tickRechargeInterval;
    private float totalRechargeTime = 0;
    private float rechargeTimer = 0f;
    private float dashTimer = 0f;
    public float FILLER;
    void Start()
    {
        currentTicks = totalTicks;

        if (playerObj == null || tickTexture == null) return;
        Movement dashCooldownSeconds = playerObj.transform.Find("Player").GetComponent<Movement>();
        if (dashCooldownSeconds == null || dashCooldownSeconds.dashCool <= 0) return;

        tickRechargeInterval = (dashCooldownSeconds.dashCool) / totalTicks; // Slightly faster recharge to offset frame delay
    }

    // Update is called once per frame
    void Update()
    {
        Movement movement = playerObj.transform.Find("Player").GetComponent<Movement>();
        if (movement == null || movement.dashDuration <= 0) return;

        // Once stamina goes to 2, the dash is currently happening- nothing happens to the bar
        if (movement.stamina == 2 && currentTicks > 0) 
        {
            currentTicks = 0;
            rechargeTimer = 0f;
            isRecharging = false;
        }

        // If player just entered cooldown phase, start recharging
        if (movement.stamina == 3)
        {
            if (!isRecharging)
            {
                isRecharging = true;
                totalRechargeTime = 0f;
            }

            totalRechargeTime += Time.deltaTime;

            // How many ticks should we have by now?
            int expectedTicks = Mathf.FloorToInt((totalRechargeTime / movement.dashCool) * totalTicks);
            expectedTicks = Mathf.Clamp(expectedTicks, 0, totalTicks);

            if (expectedTicks > currentTicks)
            {
                currentTicks = expectedTicks;
            }

            if (currentTicks >= totalTicks)
            {
                currentTicks = totalTicks;
                isRecharging = false;
            }
        }

        // Handle gradual recharge
        if (isRecharging)
        {
            rechargeTimer += Time.deltaTime;
            if (rechargeTimer >= tickRechargeInterval * FILLER)
            {
                rechargeTimer = 0f;
                currentTicks++;

                if (currentTicks >= totalTicks)
                {
                    currentTicks = totalTicks;
                    isRecharging = false;
                }
            }
        }
    }
    void OnGUI()
    {
        if (tickTexture == null) return;

        // Get current screen dimensions
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        // Convert relative values into actual pixel values
        Vector2 scaledStartPosition = new Vector2(screenWidth * relativeStartPosition.x, screenHeight * relativeStartPosition.y);
        Vector2 scaledTickSize = new Vector2(screenWidth * relativeTickSize.x, screenHeight * relativeTickSize.y);

        for (int i = 0; i < totalTicks; i++)
        {
            Rect tickRect = new Rect(
                scaledStartPosition.x + (scaledTickSize.x * i),
                scaledStartPosition.y,
                scaledTickSize.x,
                scaledTickSize.y
            );

            if (i < currentTicks)
            {
                GUI.DrawTexture(tickRect, tickTexture);
            }
        }
    }


    public bool IsStaminaFull()
    {
        return currentTicks == totalTicks;
    }

    public bool IsRecharging()
    {
        return isRecharging;
    }
}
