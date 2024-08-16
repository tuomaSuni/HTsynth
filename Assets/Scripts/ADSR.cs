using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADSR : Envelope
{
    // Amplitude and Volume
    private float amplitude = 0.0f;
    private float masterVolume = 1.0f;

    // ADSR Time and Speed Variables
    private float attackTime = 1.0f;
    private float attackSpeed = 0.1f;
    private float decayTime = 0.2f;
    private float decaySpeed = 0.15f;
    private float releaseTime = 0.10f;
    private bool logic;
    
    // Audio Components
    private AudioSource audioSource;
    private KeyLogic keyLogic;

    private void Awake()
    {
        InitializeAudioSource();
    }
    private void Start()
    {
        InitializeTheKey();
    }

    private void InitializeAudioSource()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource component is missing.");
            return;
        }

        audioSource.volume = amplitude;
    }

    private void InitializeTheKey()
    {
        keyLogic = GetComponent<KeyLogic>();
        if (keyLogic == null)
        {
            Debug.LogError("KeyLogic component is missing.");
            return;
        }
    }

    private void Update()
    {
        audioSource.volume = amplitude * masterVolume;
    }

    public IEnumerator Attack()
    {
        while (amplitude < attackTime)
        {
            amplitude += Time.deltaTime * attackSpeed;
            yield return null;
        }

        StartCoroutine(Decay());
    }

    private IEnumerator Decay()
    {
        while (amplitude > (attackTime - decayTime))
        {
            amplitude -= Time.deltaTime * decaySpeed;
            yield return null;
        }

        StartCoroutine(Sustain());
    }

    private IEnumerator Sustain()
    {
        if (logic)
        {
            yield break;
        }
        else
        {
            while (keyLogic.isPlaying)
            {
                yield return null;
            }
        }

        StartCoroutine(Release());
    }

    private IEnumerator Release()
    {
        while (amplitude > 0.0f)
        {
            amplitude -= Time.deltaTime * releaseTime;
            yield return null;
        }
    }
}