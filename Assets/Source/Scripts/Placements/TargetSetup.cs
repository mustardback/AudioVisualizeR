using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSetup : MonoBehaviour {
    public GameObject TargetSphere;
    public GameObject TargetStar;
    public GameObject TargetCubeHolder;
    public Camera MainCamera;
    public int Testing;
    private Vector3 rPosition;
    
    // Use this for initialization

    void Start () {
        //Randomize targets around camera 
        if (Testing == 1)
        {
            //rPosition = new Vector3(-5f, 0, -5f);
            TargetSphere.transform.position = MainCamera.transform.position + MainCamera.transform.right * 15;
            TargetStar.transform.position = MainCamera.transform.position - MainCamera.transform.forward * 25;
            TargetCubeHolder.transform.position = MainCamera.transform.position - MainCamera.transform.right * 20;
            //TargetStar.transform.position = TargetSphere.transform.position - new Vector3(0, -5f, -5f);
            //TargetCubeHolder.transform.position = TargetSphere.transform.position - new Vector3(-5f, -5f, 0);
        }
        else
        {
            rPosition = new Vector3(Random.Range(-15f, -10f), 0, Random.Range(-20f, -15f));
            TargetSphere.transform.position = transform.position + rPosition;
            TargetStar.transform.position = TargetSphere.transform.position - new Vector3(0, Random.Range(-5f, 5f), Random.Range(-5f, 5f));
            TargetCubeHolder.transform.position = TargetStar.transform.position - new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), 0);
        }
        //Face targets towards camera
        //TargetSphere.transform.forward = TargetSphere.transform.position - MainCamera.transform.position;
        //TargetStar.transform.forward = TargetStar.transform.position - MainCamera.transform.position; 
        TargetCubeHolder.transform.forward = MainCamera.transform.position - TargetCubeHolder.transform.position;//Rotate(TargetCubeHolder.transform.up, 90f);// (MainCamera.transform);// = TargetCubeHolder.transform.position - MainCamera.transform.position;        
        }
	
	// Update is called once per frame
	void Update () {
        
	}
}
