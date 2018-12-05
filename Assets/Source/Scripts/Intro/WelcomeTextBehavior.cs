using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WelcomeTextBehavior : MonoBehaviour {
    public Light SelectLight;
    private string[] Msgs;
    private int mIndex = 0;
    // Use this for initialization
    void Start () {
        SelectLight.intensity = 0;
        Msgs = new string[] {"To select your preset use either stick and pull the trigger to enjoy!\n (Keep pressing touchpad for more hints)", "Align the colored visualization rings with their corresponding targets.", "Look for this blue glow to indicate the selected ring node.", "Outer ring TRS are affected by inner ring TRS,\n so be mindful of your placements!", "Press Menu to return here.", "Press either touchpad to change the song.","Touch up or down on the touchpad to change the volume.", "Look around for more hints!", "What more do you want to know? Go enjoy yourself!",""};
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown("joystick button 16") || Input.GetKeyDown("joystick button 17")) 
        {
            GetComponent<TextMesh>().text = Msgs[mIndex];
            if (mIndex == 2)
            {
                float volume = 0f;
                foreach (float band in AudioAnalyzer.freqBands)
                {
                    volume += volume;
                }
                SelectLight.intensity = 0.5f + volume;
            }
            else
            {
                SelectLight.intensity = 0;
            }

            if (mIndex < Msgs.Length-1)
            {
                mIndex++;
            }
        }
    }
}
