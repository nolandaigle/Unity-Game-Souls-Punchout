using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    SaveState save;

    // Start is called before the first frame update
    void Start()
    {
        start = transform.position.x;
        end = transform.position.x;
        save = (SaveState)FindObjectOfType(typeof(SaveState));
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
                    SwitchTo(classSelected-1);
                }
            }
            else if ( Input.GetAxis("Horizontal") < 0 )
            {
                if ( classSelected < 1 && !buttonDown )
                {
                    SwitchTo(classSelected+1);
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
                if ( save.level == 1 )
                    SceneManager.LoadScene("BattleTree");
                else if ( save.level == 2 )
                    SceneManager.LoadScene("BattleTree-Level2");
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
            if ( ( save.charUnlock < 2 && classSelected == -1 ) || ( save.charUnlock < 3 && classSelected == 1 ) )
            {
                    SwitchTo(0);
            }
        }
    }

    void SaveClass(string selectedClass)
    {
        save.playerClass = selectedClass;
    }

    void SwitchTo( int toSelect )
    {
        classSelected = toSelect;
        start = transform.position.x;
        end = 20*toSelect;
        t = 0;
        BroadcastMessage("StopBounce");
        buttonDown = true;
    }
}
