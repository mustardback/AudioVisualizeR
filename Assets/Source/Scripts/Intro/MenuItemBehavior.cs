using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuItemBehavior : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(transform.position-new Vector3(0,0,1));
        transform.Rotate(Vector3.up, 180f);
	}
}
