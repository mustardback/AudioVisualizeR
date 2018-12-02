using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParamFract : MonoBehaviour {
    public int Testing;
    void Update () {
        float maxVolume = 0;
        float volume = 0;
        //float cur = mat.GetFloat("Modifier");
        foreach (float band in AudioAnalyzer.freqBands)
        {
            volume += band;
        }
        if (volume > maxVolume)
        {
            maxVolume = volume;
        }
        //Debug.Log(volume);
        if (Testing != 1)
        {
            transform.Rotate(Vector3.back, Time.deltaTime * 5 * volume);
        }
        Mesh theMesh = GetComponent<MeshFilter>().mesh;
        Vector2[] uv = theMesh.uv;

        for (int i = 0; i < uv.Length; i++)
        {
            uv[i].x += volume * 0.005f;
            uv[i].x = uv[i].x % 1;
            uv[i].y += volume * 0.005f;
            uv[i].y = uv[i].y % 1;
        }
        theMesh.uv = uv;
    }
}
