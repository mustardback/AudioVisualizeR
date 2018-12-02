using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class ParamVideo : MonoBehaviour {
    private int dir = 1;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        float volume = 0f;
        float maxVolume = 0f;
        foreach (float band in AudioAnalyzer.freqBands)
        {
            volume += band;
        }
        if (volume > maxVolume)
        {
            maxVolume = volume;            
        }
        if (volume > 0.95f*maxVolume)
        {
            dir *= -1;
        }
        transform.Rotate(Vector3.up * Time.deltaTime * 5 * volume * dir);

    }
}
