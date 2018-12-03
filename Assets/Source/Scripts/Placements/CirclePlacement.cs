using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CirclePlacement : MonoBehaviour {
    public GameObject circleCenter;
    public int radius = 5;

	// Use this for initialization
	void Start () {
        Vector3 center = transform.localPosition;
        float ang = 0;
        for (int i = 0; i < 16; i++)
        {
            GameObject sphere = GameObject.Find("parametric-sphere " + i);
            Vector3 newPos = CalculatePlacement(center, radius, ang);
            ang += 22.5f;
            sphere.transform.localPosition = newPos;
        }        
    }
	
    Vector3 CalculatePlacement(Vector3 center, float radius, float ang)
    {
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = center.y + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        pos.z = center.z;
        return pos;
    }

	// Update is called once per frame
	void Update () { 
	}
}
