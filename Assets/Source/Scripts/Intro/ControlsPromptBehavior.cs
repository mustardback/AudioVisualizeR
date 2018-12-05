using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsPromptBehavior : MonoBehaviour {
    private string[] Msgs;
    private int mIndex = 0;
    private bool[] untriggered = new bool[] {true, true, true, true, true};
    // Use this for initialization
    void Start () {
        Msgs = new string[] { "R Stick to rotate in X and Y", "L or R Grip to change selected node", "L Select Trigger to home selected to starting position", "R Select Trigger to home selected to target position", ""};
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetAxis("AXIS_1") != 0f || Input.GetAxis("AXIS_2") != 0f) {//LS to RS
            if (untriggered[0])
            {
                GetComponent<TextMesh>().text = Msgs[mIndex];
                untriggered[0] = false;
            }
        }
        if (Input.GetAxis("AXIS_3") != 0f || Input.GetAxis("AXIS_4") != 0f)//RS to Grip
        {
            if (untriggered[1])
            {
                mIndex++;
                GetComponent<TextMesh>().text = Msgs[mIndex];
                untriggered[1] = false;
            }
        }
        if (Input.GetKeyDown("joystick button 4") || Input.GetKeyDown("joystick button 5")){ //Grip to L Select
            if (untriggered[2])
            {
                mIndex++;
                GetComponent<TextMesh>().text = Msgs[mIndex];
                untriggered[2] = false;
            }
        }
        if (Input.GetAxis("AXIS_9") != 0f){ //L Select to R Select
            if (untriggered[3])
            {
                mIndex++;
                GetComponent<TextMesh>().text = Msgs[mIndex];
                untriggered[3] = false;
            }
        }
        if (Input.GetAxis("AXIS_10") != 0f) //R Select to empty
        {
            if (untriggered[4])
            {
                mIndex++;
                GetComponent<TextMesh>().text = Msgs[mIndex];
                untriggered[4] = false;
            }
        }
    }
}
