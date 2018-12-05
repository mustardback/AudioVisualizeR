using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroController : MonoBehaviour
{
    public GameObject MenuModel;
    private Transform[] childrenList;
    private List<GameObject> childObjects = new List<GameObject>();
    // Use this for initialization
    void Start()
    {
        childrenList = GetComponentsInChildren<Transform>();
        foreach (Transform child in childrenList)
        {
            childObjects.Add(child.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        GameObject Ghost = GameObject.Find("GhostCube");
        GameObject RStar = GameObject.Find("RainbowStarCube");
        GameObject Metal = GameObject.Find("MetalCube");
        GameObject Simple = GameObject.Find("SimpleCube");
        Vector3 GV = Ghost.transform.position - transform.position;
        Vector3 RV = RStar.transform.position - transform.position;
        Vector3 MV = Metal.transform.position - transform.position;
        Vector3 SV = Simple.transform.position - transform.position;
        float volume = 0f;
        foreach (float band in AudioAnalyzer.freqBands)
        {
            volume += band;
        }
        if (GV.magnitude < RV.magnitude && GV.magnitude < MV.magnitude && GV.magnitude < SV.magnitude)
        {
            float scaleVals = (volume * .01f) + 1;
            Ghost.transform.localScale = new Vector3(scaleVals, scaleVals, scaleVals);
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetAxis("AXIS_9") > 0.9f || Input.GetAxis("AXIS_10") > 0.9f)
            {
                loadlevel("Ghost");
            }
        }
        if (RV.magnitude < GV.magnitude && RV.magnitude < MV.magnitude && RV.magnitude < SV.magnitude)
        { 
            float scaleVals = (volume * .01f) + 1;
            RStar.transform.localScale = new Vector3(scaleVals, scaleVals, scaleVals);
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetAxis("AXIS_9") > 0.9f || Input.GetAxis("AXIS_10") > 0.9f)
            {
                loadlevel("RainbowStar");
            }
        }
        if (MV.magnitude < RV.magnitude && MV.magnitude < GV.magnitude && MV.magnitude < SV.magnitude)
        {
            float scaleVals = (volume * .01f) + 1;
            Metal.transform.localScale = new Vector3(scaleVals, scaleVals, scaleVals);
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetAxis("AXIS_9") > 0.9f || Input.GetAxis("AXIS_10") > 0.9f)
            {
                loadlevel("Metal");
            }
        }
        if (SV.magnitude < RV.magnitude && SV.magnitude < MV.magnitude && SV.magnitude < GV.magnitude)
        {
            float scaleVals = (volume * .01f) + 1;
            Simple.transform.localScale = new Vector3(scaleVals, scaleVals, scaleVals);
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetAxis("AXIS_9") > 0.9f || Input.GetAxis("AXIS_10") > 0.9f)
            {
                loadlevel("Simple");
            }
        }

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetAxis("AXIS_1") > 0.001f || Input.GetAxis("AXIS_4") > 0.001f)
        {
            MenuModel.transform.Rotate(Vector3.up, Time.deltaTime * 60f);
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetAxis("AXIS_1") < -0.001f || Input.GetAxis("AXIS_4") < -0.001f)
        {
            MenuModel.transform.Rotate(Vector3.up, -1 * Time.deltaTime * 60f);
        }
    }

    public void loadlevel(string level)
    {
        SceneManager.LoadScene(level);
    }
}