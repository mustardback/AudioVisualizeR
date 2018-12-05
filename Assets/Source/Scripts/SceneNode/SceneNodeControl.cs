using System.Collections;
using System.Collections.Generic; 
using UnityEngine;
using UnityEngine.UI;

public class SceneNodeControl : MonoBehaviour {
    public SceneNode TheRoot = null;
    public GameObject Home;
    public GameObject AxisFrame;
    public GameObject pLight;
    public GameObject TargetSphere, TargetStar, TargetHolder;
    List<Transform> mSelectedTransform = new List<Transform>();
    List<string> mTransformNames = new List<string>();

    private Transform mSelected;
    private int mSelectedIndex = 0;
    private GameObject xFrame, yFrame, zFrame;
    private GameObject xLight, yLight, zLight;

    // Use this for initialization
    void Start () {      
        xFrame = GameObject.Find("xAxis");
        yFrame = GameObject.Find("yAxis");
        zFrame = GameObject.Find("zAxis");
        xLight = GameObject.Find("Star_07-Trailed");
        yLight = GameObject.Find("Star_06-Trailed");
        zLight = GameObject.Find("Star_05-Trailed");
        TargetSphere = GameObject.Find("TargetSphere");
        TargetStar = GameObject.Find("TargetStar");
        TargetHolder = GameObject.Find("TargetCubeHolder");        
        mSelectedTransform.Add(TheRoot.transform);
        mTransformNames.Add("RootNode");
        GetChildrenNames(TheRoot.transform);
        SetSelectedObject(TheRoot.transform);
        TurnOffAxisFrame();
        TurnOffAxisLight(xLight);
        TurnOffAxisLight(yLight);
        TurnOffAxisLight(zLight);
    }
    //---------------------AxisFrame Calls---------------------------------------------------------
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void TurnOffAxisFrame()
    {
        xFrame.GetComponent<Renderer>().enabled = false;
        yFrame.GetComponent<Renderer>().enabled = false;
        zFrame.GetComponent<Renderer>().enabled = false;
    }

    void TurnOnAxisFrame()
    {
        xFrame.GetComponent<Renderer>().enabled = true;
        yFrame.GetComponent<Renderer>().enabled = true;
        zFrame.GetComponent<Renderer>().enabled = true;
    }

    void TurnOnAxisLight(GameObject axisLight)
    {
        axisLight.GetComponent<Renderer>().enabled = true;   
    }

    void TurnOffAxisLight(GameObject axisLight)
    {
        axisLight.GetComponent<Renderer>().enabled = false;
    }
    
    void GetChildrenNames(Transform node)
    {      
        for (int i = node.childCount - 1; i >= 0; i--)
        {
            Transform child = node.GetChild(i);
            SceneNode cn = child.GetComponent<SceneNode>();
            if (cn != null)
            {
                mTransformNames.Add(child.name);
                mSelectedTransform.Add(child);                 
                GetChildrenNames(child);
            }
        } 
    }

    // new object selected
    public void SetSelectedObject(Transform xform)
    {
        mSelected = xform;        
        if (xform != null)
        {
            Debug.Log("Selected: " + mSelected);
        }        
    }

    void Update()
    {
     //----------------------Node Selection--------------------------------------------------------
     //////////////////////////////////////////////////////////////////////////////////////////////
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            mSelectedIndex = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            mSelectedIndex = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            mSelectedIndex = 2;
        }
        mSelectedIndex %= mTransformNames.Count;
        mSelected = mSelectedTransform[mSelectedIndex];
        AxisFrame.transform.position = mSelected.transform.position;
        pLight.transform.position = mSelected.transform.position;
        Debug.Log(mTransformNames[mSelectedIndex] + " selected");
        //--------------------------------Control Scheme-------------------------------------------
        ///////////////////////////////////////////////////////////////////////////////////////////
        Vector3 V2H = mSelected.transform.position - Home.transform.position;
        Vector3 V2T = new Vector3(0f,0f,0f);
        if (mSelectedIndex == 0)
        {
            V2T = TargetSphere.transform.position - mSelected.transform.position;
        }
        if (mSelectedIndex == 1)
        {
            V2T = TargetStar.transform.position - mSelected.transform.position;
        }
        if (mSelectedIndex == 2)
        {
            V2T = TargetHolder.transform.position - mSelected.transform.position;
        }
        ///////////////////////////////////////////////////////////////////////////////////////////
        if (Input.GetKey(KeyCode.PageUp)) // Push from camera
        {
            mSelected.transform.localPosition += V2H.normalized * 0.01f;
        }
        if (Mathf.Abs(V2H.magnitude) > 5f) // Pull to camera
        {
            if (Input.GetKey(KeyCode.PageDown))
            {
                mSelected.transform.localPosition -= V2H.normalized * 0.01f;
            }
        }
        if (Input.GetKey(KeyCode.RightArrow)) //Home in on target
        {
            mSelected.transform.position += V2T * 0.01f;
        }               
        if (Input.GetKey(KeyCode.LeftArrow)) //Home out from target
        {
            mSelected.transform.localPosition -= V2T * 0.01f;
        }       
        if (Input.GetKey(KeyCode.T)) //Translate
        {            
            TurnOnAxisFrame();
            if (Input.GetKey(KeyCode.X)) //Translate X
            {         
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    //tx up
                    mSelected.transform.localPosition += mSelected.transform.right * 0.1f;
                }
                if (Input.GetKey(KeyCode.DownArrow))
                {
                    //tx down
                    mSelected.transform.localPosition -= mSelected.transform.right * 0.1f;
                }
            }
            if (Input.GetKey(KeyCode.Y)) //Translate Y
            {
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    //tx up
                    mSelected.transform.localPosition += mSelected.transform.up * 0.1f;
                }
                if (Input.GetKey(KeyCode.DownArrow))
                {
                    //tx down
                    mSelected.transform.localPosition -= mSelected.transform.up * 0.1f;
                }
            }
            if (Input.GetKey(KeyCode.Z)) //Translate Z
            {
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    //tx up
                    mSelected.transform.localPosition += mSelected.transform.forward * 0.1f;
                }
                if (Input.GetKey(KeyCode.DownArrow))
                {
                    //tx down
                    mSelected.transform.localPosition -= mSelected.transform.forward * 0.1f;
                }
            }
        }
        if (Input.GetKey(KeyCode.R)) //Rotate
        {
            //Unrotated axis values!
            float rXu = mSelected.transform.rotation.x;
            float rYu = mSelected.transform.rotation.y;
            float rZu = mSelected.transform.rotation.z;
            TurnOnAxisFrame();
            if (Input.GetKey(KeyCode.X)) //Rotate X
            {
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    //rotate up
                    float dx = rXu + 1f;
                    Quaternion q = Quaternion.AngleAxis(dx, Vector3.right);
                    mSelected.localRotation *= q;
                }
                if (Input.GetKey(KeyCode.DownArrow))
                {
                    //rotate down
                    float dx = rXu - 1f;
                    Quaternion q = Quaternion.AngleAxis(dx, Vector3.right);
                    mSelected.localRotation *= q;
                }
            }
            if (Input.GetKey(KeyCode.Y)) //Rotate Y
            {
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    //rotate up
                    float dy = rYu + 1f;
                    Quaternion q = Quaternion.AngleAxis(dy, Vector3.up);
                    mSelected.localRotation *= q;
                }
                if (Input.GetKey(KeyCode.DownArrow))
                {
                    //rotate down
                    float dy = rYu - 1f;
                    Quaternion q = Quaternion.AngleAxis(dy, Vector3.up);
                    mSelected.localRotation *= q;
                }
            }
            if (Input.GetKey(KeyCode.Z)) ////Rotate Z
            {
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    //rotate up
                    float dz = rZu + 1f;
                    Quaternion q = Quaternion.AngleAxis(dz, Vector3.forward);
                    mSelected.localRotation *= q;
                }
                if (Input.GetKey(KeyCode.DownArrow))
                {
                    //rotate down
                    float dz = rZu - 1f;
                    Quaternion q = Quaternion.AngleAxis(dz, Vector3.forward);
                    mSelected.localRotation *= q;
                }
            }
        }
        if (Input.GetKey(KeyCode.S)) //Scale
        {
            TurnOnAxisFrame();
            if (Input.GetKey(KeyCode.UpArrow)) //Scale Up
            {                
                mSelected.transform.localScale *= 1.1f;
            }
            if (Input.GetKey(KeyCode.DownArrow)) //Scale Down
            {
                mSelected.transform.localScale *= 0.9f;
            }
        }
        //----------------------AxisFrame Lighting-------------------------------------------------
        ///////////////////////////////////////////////////////////////////////////////////////////
        if (Input.GetKey(KeyCode.X))
        {
            TurnOnAxisLight(xLight);
        }
        if (Input.GetKey(KeyCode.Y))
        {
            TurnOnAxisLight(yLight);
        }
        if (Input.GetKey(KeyCode.Z))
        {
            TurnOnAxisLight(zLight);
        }
        if (!(Input.GetKey(KeyCode.T)) && !(Input.GetKey(KeyCode.R)) && !(Input.GetKey(KeyCode.S))){
            TurnOffAxisFrame();
        }
        if (!Input.GetKey(KeyCode.X)){
            TurnOffAxisLight(xLight);
        }
        if (!Input.GetKey(KeyCode.Y))
        {
            TurnOffAxisLight(yLight);
        }
        if (!Input.GetKey(KeyCode.Z))
        {
            TurnOffAxisLight(zLight);
        }
        //--------------------------WMR Controls --------------------------------------------------
        if (Input.GetAxis("AXIS_2") > 0.001f) //LS, T.Y up
        {
            TurnOnAxisFrame();
            TurnOnAxisLight(yLight);
            mSelected.transform.localPosition -= mSelected.transform.up * Input.GetAxis("AXIS_2");
        }
        if (Input.GetAxis("AXIS_2") < -0.001f) //LS, T.Y down
        {
            TurnOnAxisFrame();
            TurnOnAxisLight(yLight);
            mSelected.transform.localPosition -= mSelected.transform.up * Input.GetAxis("AXIS_2");
        }
        if (Input.GetAxis("AXIS_1") > 0.001f) //LS, T.X right
        {
            TurnOnAxisFrame();
            TurnOnAxisLight(xLight);
            mSelected.transform.localPosition += mSelected.transform.right * Input.GetAxis("AXIS_1");
        }
        if (Input.GetAxis("AXIS_1") < -0.001f) //LS, T.X left
        {
            TurnOnAxisFrame();
            TurnOnAxisLight(xLight);
            mSelected.transform.localPosition += mSelected.transform.right * Input.GetAxis("AXIS_1");
        }
        if (Input.GetAxis("AXIS_5") > 0.001f) //RS, R.Y right
        {
            TurnOnAxisFrame();
            TurnOnAxisLight(yLight);
            float rYu = mSelected.transform.rotation.y;
            float dy = rYu + 2f;
            Quaternion q = Quaternion.AngleAxis(dy, Vector3.up);
            mSelected.localRotation *= q;
        }
        if (Input.GetAxis("AXIS_5") < -0.001f) //RS, R.Y left
        {
            TurnOnAxisFrame();
            TurnOnAxisLight(yLight);
            float rYu = mSelected.transform.rotation.y;
            float dy = rYu - 2f;
            Quaternion q = Quaternion.AngleAxis(dy, Vector3.up);
            mSelected.localRotation *= q;
        }
        if (Input.GetAxis("AXIS_4") > 0.001f) //RS, R.X up
        {
            TurnOnAxisFrame();
            TurnOnAxisLight(xLight);
            float rXu = mSelected.transform.rotation.x;
            float dx = rXu + 2f;
            Quaternion q = Quaternion.AngleAxis(dx, Vector3.right);
            mSelected.localRotation *= q;
        }
        if (Input.GetAxis("AXIS_4") < -0.001f) //RS, R.X down
        {
            TurnOnAxisFrame();
            TurnOnAxisLight(xLight);
            float rXu = mSelected.transform.rotation.x;
            float dx = rXu - 2f;
            Quaternion q = Quaternion.AngleAxis(dx, Vector3.right);
            mSelected.localRotation *= q;
        }
        if (Input.GetKeyDown("joystick button 4")) //L Grip, select previous node
        {
            if (mSelectedIndex == 0)
            {
                mSelectedIndex = 2;
            }
            else
            {
                mSelectedIndex--;
            }
        }
        if (Input.GetKeyDown("joystick button 5")) //R Grip, select next node
        {
            if (mSelectedIndex == 2)
            {
                mSelectedIndex = 0;
            }
            else
            {
                mSelectedIndex++;
            }
        }
        //Homing towards target node or starting position
        mSelected.transform.position += V2T.normalized * Input.GetAxis("AXIS_10")*2f;
        mSelected.transform.localPosition += V2H.normalized * Input.GetAxis("AXIS_9")*2f;
        if ((Input.GetAxis("AXIS_2") < 0.001f && Input.GetAxis("AXIS_2") > -0.001f) && (Input.GetAxis("AXIS_1") < 0.001f && Input.GetAxis("AXIS_1") > -0.001f) && (Input.GetAxis("AXIS_5") < 0.001f && Input.GetAxis("AXIS_5") > -0.001f) && (Input.GetAxis("AXIS_4") < 0.001f && Input.GetAxis("AXIS_4") > -0.001f))
        {
            TurnOffAxisFrame();
            TurnOffAxisLight(xLight);
            TurnOffAxisLight(yLight);
            TurnOffAxisLight(zLight);
        }                
        //--------------------------AxisFrame Alignment--------------------------------------------
        ///////////////////////////////////////////////////////////////////////////////////////////
        AxisFrame.transform.position = mSelected.transform.position;
        AxisFrame.transform.rotation = mSelected.transform.rotation;
    }
}