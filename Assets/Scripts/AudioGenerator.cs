using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioGenerator : MonoBehaviour
{
    // GET FREQUENCY
	[SerializeField, ReadOnly] float octaveBaseFrequency;
    [SerializeField, ReadOnly] int k;
    [SerializeField, ReadOnly] float kFrequency;
    readonly float d12thRootOf2 = Mathf.Pow(2.0f, 1.0f / 12.0f);
    // SAMPLING
    private float sampleRate = 44100;
	private float waveLengthInSeconds = 2.0f;
    private int timeIndex = 0;
    // SOURCE
    private AudioSource audioSource;
    private WaveformGenerator Osc;
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.volume = 0.0f;
        kFrequency = octaveBaseFrequency * Mathf.Pow(d12thRootOf2, k);
        Osc = new WaveformGenerator();
    }

    void ConstructNote(float[] data, int i)
    {
        data[i] = Osc.Sine(timeIndex, kFrequency, sampleRate);
    }
	
	void OnAudioFilterRead(float[] data, int channels)
	{
		for(int i = 0; i < data.Length; i+= channels)
		{			
			ConstructNote(data, i);
			
			timeIndex++;
			
			if(timeIndex >= (sampleRate * waveLengthInSeconds))
			{
				timeIndex = 0;
			}
		}
	}
}