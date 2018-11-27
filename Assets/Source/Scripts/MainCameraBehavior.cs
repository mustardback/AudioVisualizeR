using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraBehavior : MonoBehaviour {
    public Camera MainCamera;
    //private Transform lookAtXform;

	// Use this for initialization
	void Start () {
        //lookAtXform = lookAt.transform;
	}
	
	// Update is called once per frame
	void Update () {
        //LookAt rotation
        //Vector3 V = lookAtXform.localPosition - transform.localPosition;

        //transform.LookAt(lookAt.transform);
        //Tumble
        //if (Input.GetKey(KeyCode.LeftAlt) && Input.GetMouseButton(0))
        //{
        //    transform.RotateAround(lookAtXform.position, Vector3.up, Input.GetAxis("Mouse X"));
        //    transform.RotateAround(lookAtXform.position, Vector3.right, Input.GetAxis("Mouse Y"));
        //}

        //Zoom in
        if (Input.GetKeyDown(KeyCode.W)) // && Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            Debug.Log("W");
            MainCamera.transform.position += transform.forward * 10 * Time.deltaTime;// Input.GetAxis("Mouse ScrollWheel");
        }
        
        //Zoom out
        if (Input.GetKeyDown(KeyCode.S))// && Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            Debug.Log("S");
            MainCamera.transform.position -= transform.forward * 10 * Time.deltaTime;// Input.GetAxis("Mouse ScrollWheel");
        }
    }
} 