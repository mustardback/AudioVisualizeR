using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParamSphere : MonoBehaviour {
    public int band;
    public float scale, scaleMultiplier;
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
        if (volume > 0.95f * maxVolume)
        {
            dir *= -1;
        }
        float scaleVals = (AudioAnalyzer.freqBands[band]*scaleMultiplier) + scale;
        transform.localScale = new Vector3(scaleVals, scaleVals, scaleVals);
        transform.Rotate(Vector3.back * Time.deltaTime * 5 * volume);
    }
}
