using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parameters : MonoBehaviour
{
    [Header("Programmable parameters:")]
    [Tooltip("Time it takes until audio reaches the peak.")] [Range(0, 1)] public float attackTime = 1.0f;
    [HideInInspector] public float attackSpeed = 4.0f;
    [Tooltip("Time it takes until audio starts reaching the sustain phase.")] [Range(0, 1)] public float decayTime = 0.5f;
    [HideInInspector] public float decaySpeed = 3.0f;
    [Tooltip("Time it takes until audio starts fading, 0 is infinite.")] [Range(0, 1)] public float sustainTime = 1.70f;
    [Tooltip("Time it takes to reach volume of 0")] [Range(0, 1)] public float releaseTime = 1.0f;

    private void OnValidate()
    {
        if (decayTime > attackTime)
        {
            decayTime = attackTime;
        }
    }
}
