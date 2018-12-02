using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParamFrame : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float volume = 0f;        
        foreach (float band in AudioAnalyzer.freqBands)
        {
            volume += band;
        }        
        float scaleVals = (volume * 0.01f) + 1;
        transform.localScale = new Vector3(scaleVals, scaleVals, scaleVals);
    }
}
