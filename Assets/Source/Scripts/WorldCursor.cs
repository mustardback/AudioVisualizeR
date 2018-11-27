using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldCursor : MonoBehaviour
{
    //private MeshRenderer meshRenderer;
    public Camera MainCamera;
    // Use this for initialization
    void Start()
    {
        // Grab the mesh renderer that's on the same object as this script.
        //meshRenderer = this.gameObject.GetComponentInChildren<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("update called");
        // Do a raycast into the world based on the user's
        // head position and orientation.
        Vector3 headPosition = MainCamera.transform.position;
        Vector3 gazeDirection = MainCamera.transform.forward;
        transform.position = 6 * (gazeDirection);
        //RaycastHit hitInfo;

        //if (Physics.Raycast(headPosition, gazeDirection, out hitInfo))
        //{
        //    // If the raycast hit a hologram...
        //    // Display the cursor mesh.
        //    meshRenderer.enabled = true;
        //    this.GetComponent<Renderer>().material.color = Color.yellow;
        //    // Move the cursor to the point where the raycast hit.
        //    this.transform.position = hitInfo.point;

        //    // Rotate the cursor to hug the surface of the hologram.
        //    this.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
        //}
        //else
        //{
        //    // If the raycast did not hit a hologram, hide the cursor mesh.
        //    this.GetComponent<Renderer>().material.color = Color.white;
        //    meshRenderer.enabled = false;
        //}
    }
}