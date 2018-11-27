using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtBehavior : MonoBehaviour {
    private Vector3 mPreviousSliderValues = Vector3.zero;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void alterX(float val)
    {
        float dv = val - mPreviousSliderValues.x;
        mPreviousSliderValues.x = val;
        transform.position += new Vector3(dv, 0f, 0f);
    }

    public void alterY(float val)
    {
        float dv = val - mPreviousSliderValues.y;
        mPreviousSliderValues.y = val;
        transform.position += new Vector3(0f, dv, 0f);
    }

    public void alterZ(float val)
    {
        float dv = val - mPreviousSliderValues.z;
        mPreviousSliderValues.z = val;
        transform.position += new Vector3(0f, 0f, dv);
    }
}
