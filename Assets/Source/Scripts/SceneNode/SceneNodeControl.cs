using System.Collections;
using System.Collections.Generic; 
using UnityEngine;
using UnityEngine.UI;

public class SceneNodeControl : MonoBehaviour {
    public SceneNode TheRoot = null;
    public Camera MainCamera;
    public GameObject AxisFrame;
    public GameObject pLight;
    List<Transform> mSelectedTransform = new List<Transform>();
    List<string> mTransformNames = new List<string>();

    private Transform mSelected;
    private int mSelectedIndex = 0;
    private GameObject xFrame, yFrame, zFrame;
    private GameObject xLight, yLight, zLight;
    // Use this for initialization
    void Start () {
        //AxisFrame.GetComponent<Renderer>().enabled = false;
        
        xFrame = GameObject.Find("xAxis");
        yFrame = GameObject.Find("yAxis");
        zFrame = GameObject.Find("zAxis");
        xLight = GameObject.Find("Star_07-Trailed");
        yLight = GameObject.Find("Star_06-Trailed");
        zLight = GameObject.Find("Star_05-Trailed");
        
        Debug.Assert(TheRoot != null);

        mSelectedTransform.Add(TheRoot.transform);
        mTransformNames.Add("RootNode");

        GetChildrenNames(TheRoot.transform);
        SetSelectedObject(TheRoot.transform);
        TurnOffAxisFrame();
        TurnOffAxisLight(xLight);
        TurnOffAxisLight(yLight);
        TurnOffAxisLight(zLight);
        //foreach (string n in mTransformNames){
        //    Debug.Log(mTransformNames); 
        //} 
    }
    //---------------------AxisFrame Calls-------------------------------------
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
    //-------------------------------------------------------------------------

    void GetChildrenNames(Transform node)
    {
        //Debug.Log("GCN called for node " + node.name);        
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


    //private void UISetObjectXform(ref Vector3 p, ref Quaternion q)
    //{
    //    if (mSelected == null)
    //        return;

    //    if (T.isOn)
    //    {
    //        mSelected.localPosition = p;
    //    }
    //    else if (S.isOn)
    //    {
    //        mSelected.localScale = p;
    //    }
    //    else
    //    {
    //        mSelected.localRotation *= q;
    //    }
    //}

    // new object selected
    public void SetSelectedObject(Transform xform) //SetSelectedObject(mSelectedTransform[index]);
    {
        mSelected = xform;
        
        if (xform != null)
        {
            Debug.Log("Selected: " + mSelected);// ObjectName.text = "Selected:" + xform.name;
        }        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            // scroll up
            // Debug.Log("Scroll up fired");
            //mSelectedIndex++;
            mSelectedIndex = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            // scroll up
            mSelectedIndex = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            // scroll up
            mSelectedIndex = 2;
        }
        //if (Input.GetKeyDown(KeyCode.S))
        //{
        //    // scroll down
        //    // Debug.Log("Scroll down fired");
        //    mSelectedIndex--;
        //}
        mSelectedIndex %= mTransformNames.Count;
        mSelected = mSelectedTransform[mSelectedIndex];
        AxisFrame.transform.position = mSelected.transform.position;
        pLight.transform.position = mSelected.transform.position;
        //Debug.Log(mSelectedIndex);
        Debug.Log(mTransformNames[mSelectedIndex] + " selected");

        //--------------------Control Scheme--------------------------------------------
        Vector3 V = mSelected.transform.position - MainCamera.transform.position;
        if (Input.GetKey(KeyCode.RightArrow)) //push
        {
            //Debug.Log("Zoom Out fired");
            mSelected.transform.localPosition += V * 0.01f;
            //AxisFrame.transform.position = mSelected.transform.position;
            //AxisFrame.GetComponent<Renderer>().enabled = true;
        }

        if (Mathf.Abs(V.magnitude) > 5f) //pull
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                //Debug.Log("Zoom In fired");
                mSelected.transform.localPosition -= V * 0.01f;
                //AxisFrame.transform.position = mSelected.transform.position;
                //AxisFrame.GetComponent<Renderer>().enabled = true;
            }
        }
        //if (Input.GetKey(KeyCode.Z)) //right
        //{
        //    mSelected.transform.localPosition += mSelected.transform.right * 0.1f;
        //}
        //if (Input.GetKey(KeyCode.X)) //left
        //{
        //    mSelected.transform.localPosition -= mSelected.transform.right * 0.1f; 
        //}

        //if (Input.GetKey(KeyCode.E)) //rotate about Z, fix with Quaternions
        //{
        //    mSelected.transform.Rotate(mSelected.transform.forward, 0.35f);
        //}
        //if (Input.GetKey(KeyCode.R)) //rotate about X, fix with Quaternions
        //{
        //    mSelected.transform.Rotate(mSelected.transform.right, 0.35f);
        //}
        //if (Input.GetKey(KeyCode.T)) //rotate about Y, fix with Quaternions
        //{
        //    mSelected.transform.Rotate(mSelected.transform.up, 0.35f);
        //}


        //if (Input.GetKey(KeyCode.F)) //scale up
        //{
        //    mSelected.transform.localScale *= 1.1f;
        //}
        //if (Input.GetKey(KeyCode.G)) //scale down
        //{
        //    mSelected.transform.localScale *= 0.9f;
        //}

        if (Input.GetKey(KeyCode.T)) //Translate
        {
            //axisframe.transform.position = mselected.transform.position;
            //axisframe.getcomponent<renderer>().enabled = true;
            TurnOnAxisFrame();
            if (Input.GetKey(KeyCode.X)) //Translate X
            {
                Debug.Log("translation in X fired");
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
            //Get unrotated axis values!
            float rXu = mSelected.transform.rotation.x;
            float rYu = mSelected.transform.rotation.y;
            float rZu = mSelected.transform.rotation.z;

            //AxisFrame.transform.position = mSelected.transform.position;
            //AxisFrame.GetComponent<Renderer>().enabled = true;
            TurnOnAxisFrame();
            if (Input.GetKey(KeyCode.X)) //Rotate X
            {
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    //rotate up
                    //mSelected.transform.Rotate(mSelected.transform.right, 0.5f);
                    //rX = rXu + 1f;
                    //mSelected.transform.rotation = Quaternion.Euler(rX, rYu, rZu);
                    float dx = rXu + 1f;
                    Quaternion q = Quaternion.AngleAxis(dx, Vector3.right);
                    mSelected.localRotation *= q;
                }
                if (Input.GetKey(KeyCode.DownArrow))
                {
                    //rotate down
                    //mSelected.transform.Rotate(mSelected.transform.right, -0.5f);
                    //rX = rXu - 1f;
                    //mSelected.transform.rotation = Quaternion.Euler(rX, rYu, rZu);
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
                    //mSelected.transform.Rotate(mSelected.transform.up, 0.5f);
                    //rY = rYu + 1f;
                    //mSelected.transform.rotation = Quaternion.Euler(rXu, rY, rZu);
                    float dy = rYu + 1f;
                    Quaternion q = Quaternion.AngleAxis(dy, Vector3.up);
                    mSelected.localRotation *= q;
                }
                if (Input.GetKey(KeyCode.DownArrow))
                {
                    //rotate down
                    //mSelected.transform.Rotate(mSelected.transform.up, -0.5f);
                    //rY = rYu - 1f;
                    //mSelected.transform.rotation = Quaternion.Euler(rXu, rY, rZu);
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
                    //mSelected.transform.Rotate(mSelected.transform.forward, 0.5f);
                    //rZ = rZu + 1f;
                    //mSelected.transform.rotation = Quaternion.Euler(rXu, rYu, rZ);
                    float dz = rZu + 1f;
                    Quaternion q = Quaternion.AngleAxis(dz, Vector3.forward);
                    mSelected.localRotation *= q;
                }
                if (Input.GetKey(KeyCode.DownArrow))
                {
                    //rotate down
                    //mSelected.transform.RotateAround(mSelected.transform.forward, -0.5f);
                    //rZ = rZu - 1f;
                    //mSelected.transform.rotation = Quaternion.Euler(rXu, rYu, rZ);
                    float dz = rZu - 1f;
                    Quaternion q = Quaternion.AngleAxis(dz, Vector3.forward);
                    mSelected.localRotation *= q;
                }
            }
        }
        if (Input.GetKey(KeyCode.S)) //Scale
        {
            //AxisFrame.transform.position = mSelected.transform.position;
            //AxisFrame.GetComponent<Renderer>().enabled = true;
            TurnOnAxisFrame();
            if (Input.GetKey(KeyCode.UpArrow)) //Scale Up
            {
                //scale up
                mSelected.transform.localScale *= 1.1f;
            }
            if (Input.GetKey(KeyCode.DownArrow)) //Scale Down
            {
                //scale down
                mSelected.transform.localScale *= 0.9f;
            }
        }

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
        //-----------------------------------------------------------------------------------
        // Turn off axis frame/lights
        if (!(Input.GetKey(KeyCode.T)) && !(Input.GetKey(KeyCode.R)) && !(Input.GetKey(KeyCode.S))){
            //AxisFrame.GetComponent<Renderer>().enabled = false;
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
        AxisFrame.transform.position = mSelected.transform.position;
        AxisFrame.transform.rotation = mSelected.transform.rotation;
    }
}