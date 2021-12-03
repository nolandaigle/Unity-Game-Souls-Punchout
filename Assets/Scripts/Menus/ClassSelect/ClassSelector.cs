using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassSelector : MonoBehaviour
{
    // animate the game object from -1 to +1 and back
    float start;
    float end;

    // starting value for the Lerp
    static float t = 0.0f;

    int classSelected = 0;

    public float changeSpeed = 1;

    bool buttonDown = false;

    bool isClassSelected = false;

    // Start is called before the first frame update
    void Start()
    {
        start = transform.position.x;
        end = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if ( !isClassSelected )
        {
            if ( Input.GetAxis("Horizontal") > 0 )
            {
                if ( classSelected > -1 && !buttonDown )
                {
                    classSelected -= 1;
                    start = transform.position.x;
                    end = 20*classSelected;
                    t = 0;
                    BroadcastMessage("StopBounce");
                    buttonDown = true;
                }
            }
            else if ( Input.GetAxis("Horizontal") < 0 )
            {
                if ( classSelected < 1 && !buttonDown )
                {
                    classSelected += 1;
                    start = transform.position.x;
                    end = 20*classSelected;
                    t = 0;
                    BroadcastMessage("StopBounce");
                    buttonDown = true;
                }
            }
            else
                buttonDown = false;

            if ( Input.GetButtonDown("Select") )
            {
                if ( classSelected == 0 )
                    SaveClass("Warrior");
                else if ( classSelected == 1 )
                    SaveClass("Thief");
                else if ( classSelected == -1 )
                    SaveClass("Mage");
                isClassSelected = true;
            }

        }
        
        if ( t < 1.5 && t >= 0 )
        {
            transform.position = new Vector3(Mathf.Lerp(start, end, t), 0, 0);

            t += changeSpeed * Time.deltaTime;
        }
        else if ( t >= 1)
        {
            t = -1;
            BroadcastMessage("StartBounce", true);
        }
    }

    void SaveClass(string selectedClass)
    {
        SaveState save = (SaveState)FindObjectOfType(typeof(SaveState));
        save.playerClass = selectedClass;
        save.SaveFile();
    }
}
