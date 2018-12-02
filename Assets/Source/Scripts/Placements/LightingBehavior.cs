using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingBehavior : MonoBehaviour {
    public Camera MainCamera;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = MainCamera.transform.forward;
        transform.forward = MainCamera.transform.forward;
	}
}
