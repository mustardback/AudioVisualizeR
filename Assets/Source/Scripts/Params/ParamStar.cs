using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParamStar : MonoBehaviour {
    private float width;
    private int dir = 1;
    //private Texture texture;
    //private int channel;
	// Use this for initialization
	void Start () {
        //texture = GetComponent<Renderer>().material.mainTexture;
        //channel = GetComponent<NodePrimitive>().band;
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

        //texture mod
        //Mesh theMesh = GetComponent<MeshFilter>().mesh;
        //Vector2[] uv = theMesh.uv;

        //for (int i = 0; i < uv.Length; i++)
        //{
        //    uv[i].x += AudioAnalyzer.freqBands[channel] * 0.01f;
        //    uv[i].x = uv[i].x % 1;
        //    uv[i].y += AudioAnalyzer.freqBands[channel] * 0.01f;
        //    uv[i].y = uv[i].y % 1;
        //}
        //theMesh.uv = uv;


        //reset for song change
        if (Input.GetKeyDown(KeyCode.Space))
        {
            maxVolume = 0;
        }
    }
}
