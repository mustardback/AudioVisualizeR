using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlignmentTest : MonoBehaviour {
    public GameObject SphereNode, StarNode, HolderNode;        
    public GameObject SphereTarget, StarTarget, HolderTarget;
    public AudioSource AudioSource;
    private GameObject NodeLight,SphereLight, StarLight, HolderLight;    
    private float dis;
    private bool[] aligned = { false, false, false };
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
            SphereLight.GetComponent<Light>().color = new Color(1f,0,1f);
            float volume = 0f;
            foreach (float band in AudioAnalyzer.freqBands)
            {
                volume += band;
            }
            float scaleVals = 1 + 0.01f * volume;
            SphereTarget.transform.localScale = new Vector3(scaleVals, scaleVals, scaleVals);
            aligned[0] = true;
        }
        

        if (starV.magnitude < dis)
        {
            //Indicate star alignment
            //Debug.Log("Stars aligned");
            StarLight.GetComponent<Light>().color = new Color(1f, 0, 1f);
            float volume = 0f;
            foreach (float band in AudioAnalyzer.freqBands)
            {
                volume += band;
            }
            float scaleVals = 1 + 0.01f * volume;
            StarTarget.transform.localScale = new Vector3(scaleVals, scaleVals, scaleVals);
            StarTarget.transform.Rotate(Vector3.up, Time.deltaTime * 10 * volume);
            aligned[1] = true;
        }
        
        if (holderV.magnitude < dis)
        {
            //Indicate holder alignment
            //Debug.Log("Holders aligned");
            HolderLight.GetComponent<Light>().color = new Color(1f, 0, 1f);
            float volume = 0f;
            foreach (float band in AudioAnalyzer.freqBands)
            {
                volume += band;
            }            
            HolderTarget.transform.Rotate(Vector3.up, Time.deltaTime * 10 * volume);
            aligned[2] = true;
        }
        

        if (aligned[0] == true && aligned[1] == true && aligned[2] == true)
        {
            //Debug.Log("Total Alignment Achieved");
            SphereLight.GetComponent<Light>().color = new Color(0,1f,1f);
            StarLight.GetComponent<Light>().color = new Color(0, 1f, 1f);
            HolderLight.GetComponent<Light>().color = new Color(0, 1f, 1f);
            NodeLight.GetComponent<Light>().color = new Color(0, 1f, 1f);

            GameObject rCube = GameObject.Find("RewardCube");
            
            rCube.transform.localScale = new Vector3(150f, 150f, 150f);
            float volume = 0f;
            foreach (float band in AudioAnalyzer.freqBands)
            {
                volume += band;
            }
            rCube.transform.Rotate(Vector3.up, volume*0.001f);
            //rCube.transform.Rotate(Vector3.right, 3f + volume * 0.001f);
            //rCube.transform.Rotate(Vector3.forward, 7f + volume * 0.001f);
            rCube.transform.position = GameObject.Find("TrackTitle").transform.position;

        }
    }
}
