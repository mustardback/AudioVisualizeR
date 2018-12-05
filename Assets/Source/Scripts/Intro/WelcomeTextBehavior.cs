﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WelcomeTextBehavior : MonoBehaviour {
    private string[] Msgs;
    private int mIndex = 0;
    // Use this for initialization
    void Start () {
        Msgs = new string[] {"To select your preset use either stick and pull the trigger to enjoy!\n (Keep pressing touchpad for more hints)", "Align the colored visualization rings with their corresponding targets.", "Look for a blue glow to indicate the selected ring node.", "Outer ring TRS are affected by inner ring TRS,\n so be mindful of your placements!", "Press Menu to return here.", "Press either touchpad to change the song.","Touch up or down on the touchpad to change the volume.", "What more do you want to know? Go enjoy yourself!"};
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown("joystick button 16") || Input.GetKeyDown("joystick button 17")) 
        {
            GetComponent<TextMesh>().text = Msgs[mIndex];
            if (mIndex < Msgs.Length-1)
            {
                mIndex++;
            }
        }
    }
}
