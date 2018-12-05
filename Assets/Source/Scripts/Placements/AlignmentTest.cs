using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlignmentTest : MonoBehaviour {
    public GameObject SphereNode, StarNode, HolderNode;        
    public GameObject SphereTarget, StarTarget, HolderTarget;
    public AudioSource AudioSource;
    private GameObject NodeLight,SphereLight, StarLight, HolderLight;    
    private float dis;
    
    // Use this for initialization
    void Start () {
        dis = 1f;
        SphereLight = GameObject.Find("Sphere Point Light");
        StarLight = GameObject.Find("Star Point Light");
        HolderLight = GameObject.Find("Holder Point Light");
        NodeLight = GameObject.Find("Point Light");
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 sphereV = SphereNode.transform.position - SphereTarget.transform.position;
        Vector3 starV = StarNode.transform.position - StarTarget.transform.position;
        Vector3 holderV = HolderNode.transform.position - HolderTarget.transform.position;

        if (sphereV.magnitude < dis)
        {
            //Indicate sphere alignment
            //Debug.Log("Spheres aligned");            
            SphereLight.GetComponent<Light>().color = Color.green;
        }
        

        if (starV.magnitude < dis)
        {
            //Indicate star alignment
            //Debug.Log("Stars aligned");
            StarLight.GetComponent<Light>().color = Color.green;
            float volume = 0f;
            foreach (float band in AudioAnalyzer.freqBands)
            {
                volume += band;
            }
            float scaleVals = 1 + 0.01f * volume;
            StarTarget.transform.localScale = new Vector3(scaleVals, scaleVals, scaleVals);
        }
        
        if (holderV.magnitude < dis)
        {
            //Indicate holder alignment
            //Debug.Log("Holders aligned");
            HolderLight.GetComponent<Light>().color = Color.green;
            float volume = 0f;
            foreach (float band in AudioAnalyzer.freqBands)
            {
                volume += band;
            }            
            HolderTarget.transform.Rotate(Vector3.up, Time.deltaTime * 10 * volume);
        }
        

        if ((sphereV.magnitude < dis) && (starV.magnitude < dis) && (holderV.magnitude < dis))
        {
            //Debug.Log("Total Alignment Achieved");
            SphereLight.GetComponent<Light>().color = new Color(0.5411f, 0.0588f, 0.3372f);
            StarLight.GetComponent<Light>().color = new Color(0.5411f, 0.0588f, 0.3372f);
            HolderLight.GetComponent<Light>().color = new Color(0.5411f, 0.0588f, 0.3372f);
            NodeLight.GetComponent<Light>().color = new Color(0.5411f, 0.0588f, 0.3372f);
        }
    }
}
