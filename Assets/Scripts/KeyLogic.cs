using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyLogic : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private ADSR adsr;
    private Color iconColor;
    [HideInInspector] public bool isActive;
    private void Start()
    {
        InitializeSpriteRenderer();
        InitializeIconColor();
    }

    private void InitializeSpriteRenderer()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer component is missing.");
        }
    }

    private void InitializeIconColor()
    {
        if (spriteRenderer != null)
        {
            iconColor = spriteRenderer.color;
            SetActive(false, 0.7f);
        }
    }

    public void SetActive(bool active, float alpha)
    {
        isActive = active;

        if (spriteRenderer != null)
        {
            iconColor.a = alpha;
            spriteRenderer.color = iconColor;
        }
    }
}