using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TickHealthBar : MonoBehaviour
{
    public GameObject playerObj; // Reference to PlayerObj
    public Texture2D tickTexture; // Green/Blue Bar texture for the ticks
    public int maxTicks = 50; // Total number of tick boxes
    public Color edgeGlowColor = new Color(1f, 0f, 0f, 0.5f);

    private Vector2 relativeStartPosition = new Vector2(0.05f, 0.075f); // 5% from top-left
    private Vector2 relativeTickSize = new Vector2(0.0125f, 0.05f); // Tick size relative to screen

    // Variables for fade and growth effect after health is below 30%
    private float fadeDuration = 2f; // Time to fully fade in
    private float maxGlowSize = 30f; // Max size for the glow edges
    private float currentGlowSize = 0f; // Current size of the glow edges
    private float currentAlpha = 0f; // Current alpha (opacity) for the glow

    private float fadeTimer = 0f; // Timer to track fade progress

    void OnGUI()
    {
        if (playerObj == null || tickTexture == null) return;

        Life playerLife = playerObj.transform.Find("Player").GetComponent<Life>();
        if (playerLife == null || playerLife.amount_max <= 0) return;

        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        Vector2 startPosition = new Vector2(screenWidth * relativeStartPosition.x, screenHeight * relativeStartPosition.y);
        Vector2 tickSize = new Vector2(screenWidth * relativeTickSize.x, screenHeight * relativeTickSize.y);
        
        float healthRatio = Mathf.Clamp01(playerLife.amount / playerLife.amount_max);
        int currentTicks = Mathf.RoundToInt(healthRatio * maxTicks);
        
        if (healthRatio < 0.3f)
        {
            // Start fading in and growing the glow
            FadeInGlow();
            DrawEdgeGlow(); // Draw the glow with the updated size and transparency
        }
        else
        {
            // Reset the fade effect when health is above 30%
            currentGlowSize = 0f;
            currentAlpha = 0f;
            fadeTimer = 0f;
        }

        for (int i = 0; i < maxTicks; i++)
        {
            Rect tickRect = new Rect(
                startPosition.x + (tickSize.x) * i,
                startPosition.y,
                tickSize.x,
                tickSize.y
            );

            if (i < currentTicks)
            {
                GUI.DrawTexture(tickRect, tickTexture);
            }
        }
    }

    void DrawEdgeGlow()
    {
        // Set the color for the glow
        GUI.color = edgeGlowColor;

        // Draw glow around the screen edges
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        // Top border
        GUI.DrawTexture(new Rect(0, 0, screenWidth, 10), Texture2D.whiteTexture);
        // Bottom border
        GUI.DrawTexture(new Rect(0, screenHeight - 10, screenWidth, 10), Texture2D.whiteTexture);
        // Left border
        GUI.DrawTexture(new Rect(0, 0, 10, screenHeight), Texture2D.whiteTexture);
        // Right border
        GUI.DrawTexture(new Rect(screenWidth - 10, 0, 10, screenHeight), Texture2D.whiteTexture);

        // Reset the GUI color to default after drawing
        GUI.color = Color.white;
    }

    // Method to handle the fade-in and growing effect
    void FadeInGlow()
    {
        fadeTimer += Time.deltaTime;

        // Lerp for the size of the glow (making it grow over time)
        currentGlowSize = Mathf.Lerp(0f, maxGlowSize, fadeTimer / fadeDuration);

        // Lerp for the alpha (opacity) of the glow (making it fade in)
        currentAlpha = Mathf.Lerp(0f, 0.5f, fadeTimer / fadeDuration); // Alpha goes from 0 to 0.5 for semi-transparency

        // Clamp the values to make sure they don't exceed the limits
        currentGlowSize = Mathf.Clamp(currentGlowSize, 0f, maxGlowSize);
        currentAlpha = Mathf.Clamp(currentAlpha, 0f, 0.5f);
    }
}

