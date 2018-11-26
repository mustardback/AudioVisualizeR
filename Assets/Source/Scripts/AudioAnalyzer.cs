using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioAnalyzer : MonoBehaviour {
    AudioSource audioSource;                 //Waveform in question
    public static float[] samples = new float[512]; //Index 0 is bass, 512 is brilliance
    public static float[] freqBands = new float[16];

	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        GetSpectrumAudioSource();
        MakeFreqBands();
	}

    //Listens to spectrum data and store in samples array
    void GetSpectrumAudioSource()
    {
        audioSource.GetSpectrumData(samples, 0, FFTWindow.Blackman);
    }

    //Gathers spectral samples into bands and stores in freqBands array
    void MakeFreqBands()
    {
        int count = 0;
        float avg = 0;
        for (int i = 0; i < 8; i++)
        {
            int sampleCount = (int)Mathf.Pow(2, i)*2; //Roughly approximates audio bands
            for (int j = 0; j < sampleCount; j++)
            {
                avg += samples[count] * (count + 1);
                count++;
            }
            avg /= count;

            freqBands[i*2] = avg * 10;
        }
        //spheres 0,2,4,6,8,10,12,14,16 set, interpolate 1,3,5,7,9,11,13,15
        for (int i = 1; i < 16; i++)
        {
            if (i != 15)
            {
                freqBands[i] = (freqBands[i - 1] + freqBands[i + 1]) / 2;
            }
            if (i == 15)
            {
                freqBands[i] = (freqBands[14] + freqBands[0]) / 2; //This mixes high/low bands but smooths transitions.
            }
            i++;
        }
    }
}
