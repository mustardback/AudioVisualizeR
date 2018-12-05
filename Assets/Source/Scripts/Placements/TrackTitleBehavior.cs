using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackTitleBehavior : MonoBehaviour {
    //private GameObject MainCamera;
    // Use this for initialization
    public AudioSource AudioSource;
    private string title;
    private float delay = 0f;
    void Start () {
        GetComponent<TextMesh>().text = AudioSource.clip.name;
    }

    // Update is called once per frame
    void Update()
    {        
        //Display and fade after 3 secs
        if (delay < 5f)
        {
            GetComponent<TextMesh>().text = AudioSource.clip.name;
        }
        else
        {
            GetComponent<TextMesh>().text = "";
        }
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown("joystick button 16") || Input.GetKeyDown("joystick button 17") || !AudioSource.isPlaying)//reset delay
        {           
            delay = 0f;            
        }
        delay += Time.deltaTime;
    }
    
}
