using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioGenerator : MonoBehaviour
{
    // Frequency Constants
    [SerializeField, ReadOnly] private float octaveBaseFrequency = 110f;
    [SerializeField, ReadOnly] private int k;
    [SerializeField, ReadOnly] private float kFrequency;
    private const float d12thRootOf2 = 1.059463094359f; // Precomputed value for European Octave Range.

    // Sampling Constants
    private const float sampleRate = 44100f;
    private const float waveLengthInSeconds = 2.0f;
    private int timeIndex = 0;

    // Audio Source and Waveform Generator
    private AudioSource audioSource;
    private WaveformGenerator oscillator;

    private void Start()
    {
        InitializeAudioSource();
        InitializeFrequency();

        oscillator = new WaveformGenerator();
    }

    private void InitializeAudioSource()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource component is missing.");
            return;
        }
        
        audioSource.Play();
    }

    private void InitializeFrequency()
    {
        k = transform.GetSiblingIndex() + 1;
        kFrequency = octaveBaseFrequency * Mathf.Pow(d12thRootOf2, k);
    }

    private void ConstructNoteAtIndex(float[] data, int index)
    {
        data[index + 0] = oscillator.Sine(timeIndex, kFrequency, sampleRate);
        data[index + 1] = oscillator.Sawtooth(timeIndex, kFrequency, sampleRate);
    }

    private void OnAudioFilterRead(float[] data, int channels)
    {
        for (int i = 0; i < data.Length; i += channels)
        {
            ConstructNoteAtIndex(data, i);

            timeIndex = (timeIndex + 1) % (int)(sampleRate * waveLengthInSeconds);
        }
    }
}