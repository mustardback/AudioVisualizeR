﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParamStar : MonoBehaviour {
    private float width;
    private int dir = 1;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        float maxVolume = 0;
        float volume = 0;

        //get the volume and record max
        foreach (float band in AudioAnalyzer.freqBands)
        {
            volume += band;
        }
        if (volume > maxVolume)
        {
            maxVolume = volume;
        }
        //Debug.Log(volume);
        transform.Rotate(Vector3.back * Time.deltaTime * 0.0001f * volume * dir);

        //dance monkey
        if (volume > 10)
        {
            if (volume > maxVolume * 0.9f)
            {
                dir *= -1;
            }
        }
        //reset for song change
        if (Input.GetKeyDown(KeyCode.Space))
        {
            maxVolume = 0;
        }
    }
}
