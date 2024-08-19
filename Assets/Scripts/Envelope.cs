using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Envelope : MonoBehaviour
{
    // Amplitude and Volume
    protected float amplitude
    {
        get { return _amplitude; }
        set { _amplitude = Mathf.Clamp(value, 0.0f, 1.0f); }
    }
    protected float _amplitude = 0.0f;
    protected float masterVolume = 1.0f;

    // Audio Components
    private AudioSource audioSource;
    
    private void Awake()
    {
        InitializeAudioSource();
    }

    private void Update()
    {
        audioSource.volume = amplitude * masterVolume;
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
}
