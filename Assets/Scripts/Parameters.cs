using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parameters : MonoBehaviour
{
    [Header("Programmable parameters:")]
    [SerializeField] [Tooltip("Time it takes until audio reaches the peak.")] public float attackTime = 1.0f;
    public float attackSpeed = 2.0f;
    [SerializeField] [Tooltip("Time it takes until audio starts reaching the sustain phase.")] public float decayTime = 0.5f;
    public float decaySpeed = 1.30f;
    [SerializeField] [Tooltip("Time it takes until audio starts fading, 0 is infinite.")] public float sustainTime = 1.70f;
    [SerializeField] [Tooltip("Time it takes to reach volume of 0")] public float releaseTime = 1.0f;
}
