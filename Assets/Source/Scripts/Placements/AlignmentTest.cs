using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlignmentTest : MonoBehaviour {
    public GameObject SphereNode;
    public GameObject StarNode;
    public GameObject HolderNode;
    public GameObject SphereTarget;
    public GameObject StarTarget;
    public GameObject HolderTarget;
    private GameObject SphereLight, StarLight, HolderLight;
    private float dis;
    // Use this for initialization
    void Start () {
        dis = 5f;
        SphereLight = GameObject.Find("Sphere Point Light");
        StarLight = GameObject.Find("Star Point Light");
        HolderLight = GameObject.Find("Holder Point Light");
        SphereLight.SetActive(false);
        StarLight.SetActive(false);
        HolderLight.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 sphereV = SphereNode.transform.position - SphereTarget.transform.position;
        Vector3 starV = StarNode.transform.position - StarTarget.transform.position;
        Vector3 holderV = HolderNode.transform.position - HolderTarget.transform.position;

        if (sphereV.magnitude < dis)
        {
            SphereLight.SetActive(true);
        }
        else
        {
            SphereLight.SetActive(false);
        }

        if (starV.magnitude < dis)
        {
            StarLight.SetActive(true);
        }
        else
        {
            StarLight.SetActive(false);
        }

        if (holderV.magnitude < dis)
        {
            HolderLight.SetActive(true);
        }
        else
        {
            HolderLight.SetActive(false);
        }

        if ((sphereV.magnitude < dis) && (starV.magnitude < dis) && (holderV.magnitude < dis))
        {
            Debug.Log("Alignment Achieved");
        }
    }
}
