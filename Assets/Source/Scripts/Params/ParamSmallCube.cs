using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParamSmallCube : MonoBehaviour
{
    public int band;
    //public float scale, scaleMultiplier;
    private int dir = 1;
    // Use this for initialization 
    void Start()
    {
        transform.up = GameObject.Find("Target").transform.up;
    }

    // Update is called once per frame
    void Update()
    {

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

        if (band % 2 == 0)
        {
            transform.Rotate(Vector3.up, Time.deltaTime * 20 * volume);
            transform.Rotate(Vector3.right, Time.deltaTime * 20 * volume);
        }
        else
        {
            transform.Rotate(Vector3.up, Time.deltaTime * -20 * volume);
            transform.Rotate(Vector3.right, Time.deltaTime * -20 * volume);
        }
        float scaleVals = (AudioAnalyzer.freqBands[band] * 0.1f) + 0.25f;
        transform.localScale = new Vector3(scaleVals, scaleVals, scaleVals);
    }
}