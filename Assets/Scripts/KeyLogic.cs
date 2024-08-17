using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyLogic : MonoBehaviour
{
    [HideInInspector] public bool isPlaying = false;

    private ADSR adsr;
    private SpriteRenderer spriteRenderer;
    private Color iconColor;

    private void Start()
    {
        InitializeComponents();
        InitializeIconColor();
    }

    private void InitializeComponents()
    {
        adsr = GetComponent<ADSR>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (adsr == null)
        {
            Debug.LogError("ADSR component is missing. Select an envelope to use.");
        }

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

    private void OnTriggerEnter(Collider other)
    {
        if (adsr != null)
        {
            StartCoroutine(adsr.Attack());
        }
        else
        {

        }

        PointLogic pointLogic = other.GetComponent<PointLogic>();

        if (pointLogic != null)
        {
            StartCoroutine(pointLogic.CalculateForce());
        }

        isPlaying = true;
        SetAlpha(0.5f);
    }

    private void OnTriggerExit(Collider other)
    {
        SetAlpha(0.7f);
    }

    public void SetAlpha(float alpha)
    {
        if (spriteRenderer != null)
        {
            iconColor.a = alpha;
            spriteRenderer.color = iconColor;
        }
    }
}