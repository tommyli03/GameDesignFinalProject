#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

/**
 * Members: Eric, Lucas, Thomas
 * Summary: Editor utility that converts all Standard and Legacy materials in the project to URP-compatible shaders.
 * Accessible via the Unity Editor menu under Tools > Force Convert Materials to URP.
 * Updates shaders in-place to URP Lit or Unlit equivalents and logs the number of materials converted.
 */
public static class URPConversionUtility
{
    [MenuItem("Tools/Force Convert Materials to URP")]
    public static void ConvertMaterialsToURP()
    {
        // Find all materials in the project
        string[] materialGuids = AssetDatabase.FindAssets("t:Material");
        int convertedCount = 0;

        foreach (string guid in materialGuids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            Material material = AssetDatabase.LoadAssetAtPath<Material>(path);

            // Skip materials already using URP shaders
            if (material.shader.name.Contains("Universal Render Pipeline"))
                continue;

            // Convert standard shaders to URP equivalents
            if (material.shader.name == "Standard" || material.shader.name.StartsWith("Legacy Shaders/"))
            {
                material.shader = Shader.Find("Universal Render Pipeline/Lit");
                convertedCount++;
            }
            // Handle unlit shaders
            else if (material.shader.name == "Unlit/Color" || material.shader.name == "Unlit/Texture")
            {
                material.shader = Shader.Find("Universal Render Pipeline/Unlit");
                convertedCount++;
            }
        }

        Debug.Log($"Converted {convertedCount} materials to URP.");
    }
}
#endif