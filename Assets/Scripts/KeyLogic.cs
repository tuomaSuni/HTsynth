using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyLogic : MonoBehaviour
{
    [HideInInspector] public bool isPlaying = false;

    private ADSR adsr;
    private SpriteRenderer icon;
    private Color color;

    private void Start()
    {
        InitializeComponents();
        InitializeColor();
    }

    private void InitializeComponents()
    {
        adsr = GetComponent<ADSR>();
        icon = GetComponent<SpriteRenderer>();
    }

    private void InitializeColor()
    {
        color = icon.color;
        color.a = 0.7f;
        icon.color = color;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        adsr.StartCoroutine(adsr.Attack());

        PointLogic pointLogic = other.gameObject.GetComponent<PointLogic>();
        pointLogic.StartCoroutine(pointLogic.CalculateForce());

        isPlaying = true;
        UpdateAlpha(0.5f);
    }

    private void OnTriggerExit(Collider other)
    {
        UpdateAlpha(0.7f);
    }

    private void UpdateAlpha(float alpha)
    {
        color.a = alpha;
        icon.color = color;
    }
}