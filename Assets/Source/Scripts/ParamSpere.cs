using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParamSpere : MonoBehaviour {
    public int band;
    public float scale, scaleMultiplier;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float scaleVals = (AudioAnalyzer.freqBands[band]*scaleMultiplier) + scale;
        transform.localScale = new Vector3(scaleVals, scaleVals, scaleVals);
	}
}
