using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WaveformGenerator
{
    public float Sine(int timeIndex, float frequency, float sampleRate)
    {
        return Mathf.Sin(2 * Mathf.PI * timeIndex * frequency / sampleRate);
    }
    
    public float Sawtooth(int timeIndex, float frequency, float sampleRate)
    {
        float period = sampleRate / frequency;
        return 2.0f * (timeIndex / period - Mathf.Floor(timeIndex / period + 0.5f));
    }

    public float Square(int timeIndex, float frequency, float sampleRate)
    {
        float sineValue = Mathf.Sin(2 * Mathf.PI * timeIndex * frequency / sampleRate);
        return sineValue >= 0 ? 1.0f : -1.0f;
    }

    public float Triangle(int timeIndex, float frequency, float sampleRate)
    {
        float period = sampleRate / frequency;
        return 2.0f * Mathf.Abs(2.0f * (timeIndex / period - Mathf.Floor(timeIndex / period + 0.5f))) - 1.0f;
    }
}
