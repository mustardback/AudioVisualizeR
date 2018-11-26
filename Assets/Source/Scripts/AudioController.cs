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
        songs[0] = song1; //pubt the songs in the list
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
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
        //-------------------------------------------------------------
    }
}
