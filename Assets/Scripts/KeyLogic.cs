using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyLogic : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private ADSR adsr;
    private Color iconColor;

    private void Awake()
    {
        InitializeADSR();
    }
    private void Start()
    {
        InitializeSpriteRenderer();
        InitializeIconColor();
    }

    private void InitializeADSR()
    {
        adsr = GetComponent<ADSR>();

        if (adsr == null)
        {
            Debug.LogError("ADSR component is missing. Select an envelope to use.");
        }
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
            SetAlpha(0.7f);
        }
    }

    public void SetAlpha(float alpha)
    {
        if (spriteRenderer != null)
        {
            iconColor.a = alpha;
            spriteRenderer.color = iconColor;
        }
    }

    public void OnEnter()
    {
        adsr.StartCoroutine(adsr.Attack());
        SetAlpha(0.5f);
    }

    public void OnExit()
    {
        SetAlpha(0.7f);
    }
}