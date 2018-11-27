﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodePrimitive: MonoBehaviour {
    public Color MyColor = new Color(0.1f, 0.1f, 0.2f, 1.0f);
    public Vector3 Pivot;
    public int band;
    public float scale;
	// Use this for initialization
	void Start () {
    }

    void Update()
    {
    }
	
  
	public void LoadShaderMatrix(ref Matrix4x4 nodeMatrix)
    {
        float scaleVals = (AudioAnalyzer.freqBands[band]) + scale;
        Matrix4x4 p = Matrix4x4.TRS(Pivot, Quaternion.identity, Vector3.one);
        Matrix4x4 invp = Matrix4x4.TRS(-Pivot, Quaternion.identity, Vector3.one);
        Matrix4x4 trs = Matrix4x4.TRS(transform.localPosition, transform.localRotation, new Vector3(scaleVals, scaleVals, scaleVals));
        Matrix4x4 m = nodeMatrix * p * trs * invp;
        if (GetComponent<Renderer>() != null)
        {
            GetComponent<Renderer>().material.SetMatrix("MyXformMat", m);
            GetComponent<Renderer>().material.SetColor("MyColor", MyColor);
        }
    }
}