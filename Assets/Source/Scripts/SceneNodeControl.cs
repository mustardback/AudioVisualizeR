using System.Collections;
using System.Collections.Generic; 
using UnityEngine;
using UnityEngine.UI;

public class SceneNodeControl : MonoBehaviour {
    public SceneNode TheRoot = null;
    public Camera MainCamera;

    List<Transform> mSelectedTransform = new List<Transform>();
    List<string> mTransformNames = new List<string>();

    private Transform mSelected;
    private int mSelectedIndex = 0;
    // Use this for initialization
    void Start () {

        Debug.Assert(TheRoot != null);

        mSelectedTransform.Add(TheRoot.transform);
        mTransformNames.Add("RootNode");

        GetChildrenNames(TheRoot.transform);
        SetSelectedObject(TheRoot.transform);
        foreach (string n in mTransformNames){
            Debug.Log(mTransformNames); 
        } 
    } 

    void GetChildrenNames(Transform node)
    {
        Debug.Log("GCN called for node " + node.name);
        //string space = blanks;
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
        if (Input.GetKeyDown(KeyCode.W))
        {
            // scroll up
            //Debug.Log("Scroll up fired");
            mSelectedIndex++;
        }
        //if (Input.GetKeyDown(KeyCode.S))
        //{
        //    // scroll down
        //    // Debug.Log("Scroll down fired");
        //    mSelectedIndex--;
        //}
        mSelectedIndex %= mTransformNames.Count;
        //mSelectedIndex = Mathf.Abs(mSelectedIndex);

        mSelected = mSelectedTransform[mSelectedIndex];
        Debug.Log(mSelectedIndex);
        Debug.Log(mTransformNames[mSelectedIndex] + " selected");
        //Vector3 headPosition = MainCamera.transform.position;
        //Vector3 gazeDirection = MainCamera.transform.forward;

        //RaycastHit hitInfo;

        //if (Physics.Raycast(headPosition, gazeDirection, out hitInfo))
        //{
        //    // If the raycast hit a hologram...
        //    // Display the cursor mesh.
        //    //meshRenderer.enabled = true;
        //    //this.GetComponent<Renderer>().material.color = Color.yellow;
        //    // Move the cursor to the point where the raycast hit.
        //    //this.transform.position = hitInfo.point;

        //    // Rotate the cursor to hug the surface of the hologram.
        //    //this.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
        //}
        Vector3 V = mSelected.transform.position - MainCamera.transform.position;

        if (Input.GetKey(KeyCode.A)) //push
        {
            Debug.Log("Zoom Out fired");
            mSelected.transform.localPosition += V * 0.01f;
        }

        if (Mathf.Abs(V.magnitude) > 5f) //pull
        {
            if (Input.GetKey(KeyCode.D))
            {
                Debug.Log("Zoom In fired");
                mSelected.transform.localPosition -= V * 0.01f;
            }
        }
        if (Input.GetKey(KeyCode.Z)) //right
        {
            mSelected.transform.localPosition += mSelected.transform.right * 0.1f;
        }
        if (Input.GetKey(KeyCode.X)) //left
        {
            mSelected.transform.localPosition -= mSelected.transform.right * 0.1f;
        }

        if (Input.GetKey(KeyCode.E)) //rotate about Z, fix with Quaternions
        {
            mSelected.transform.Rotate(mSelected.transform.forward, 0.35f);
        }
        if (Input.GetKey(KeyCode.R)) //rotate about X, fix with Quaternions
        {
            mSelected.transform.Rotate(mSelected.transform.right, 0.35f);
        }
        if (Input.GetKey(KeyCode.T)) //rotate about Y, fix with Quaternions
        {
            mSelected.transform.Rotate(mSelected.transform.up, 0.35f);
        }
        

        if (Input.GetKey(KeyCode.F)) //scale up
        {
            mSelected.transform.localScale *= 1.1f;
        }
        if (Input.GetKey(KeyCode.G)) //scale down
        {
            mSelected.transform.localScale *= 0.9f;
        }
    }
}
