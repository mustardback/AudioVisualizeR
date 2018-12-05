using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioController : MonoBehaviour {
    private AudioClip[] songs = new AudioClip[5];
    public AudioSource AudioSource;
    public AudioClip song1;
    public AudioClip song2;
    public AudioClip song3;
    public AudioClip song4;
    public AudioClip song5;
    private int trackIndex;
    // Use this for initialization
    void Start () {
        songs[0] = song1; //put the songs in the list
        songs[1] = song2;
        songs[2] = song3;
        songs[3] = song4;
        songs[4] = song5;
        trackIndex = 0;
        AudioSource.clip = songs[trackIndex];
    }
	
	// Update is called once per frame
	void Update () {
        //------------------Track Change-------------------------------
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown("joystick button 16") || Input.GetKeyDown("joystick button 17") || !AudioSource.isPlaying)
        {
            Debug.Log("Next Track");
            trackIndex++;
            if (!(trackIndex >= 5))
            {
                AudioSource.clip = songs[trackIndex];
                AudioSource.Play();
            }
            if (trackIndex == 5)
            {
                trackIndex = 0;
                AudioSource.clip = songs[trackIndex];
                AudioSource.Play();
            }
        }
        //-----------------Volume Change-------------------------------
        if (Input.GetKeyDown(KeyCode.KeypadPlus) || Input.GetAxis("AXIS_18") < -0.001f || Input.GetAxis("AXIS_19") < -0.001f)
        {
            AudioSource.volume *= 1.1f;
        }
        if (Input.GetKeyDown(KeyCode.KeypadMinus) || Input.GetAxis("AXIS_18") > 0.001f || Input.GetAxis("AXIS_19") > 0.001f)
        {
            if (AudioSource.volume > 0.1f)
            {
                AudioSource.volume *= 0.1f;
            }
        }
    }
}
