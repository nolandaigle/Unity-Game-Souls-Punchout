using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter_Player : Fighter_Base
{
    // Start is called before the first frame update
    override protected void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    override protected void Update()
    {
        if ( currentState != State.Dead )
        {
            if ( Input.GetButtonDown("RightHandAction") )
            {
                RightHandAction();
            }

            if ( Input.GetButtonDown("LeftHandAction") )
            {
                LeftHandAction(true);
            }
            if ( Input.GetButtonUp("LeftHandAction") )
            {
                Stand();
            }
            if ( Input.GetButtonDown("Dodge") )
            {
                Dodge();
            }
        }

    	base.Update();
    }

    RightHandPrep()
    {
        anim.Play(newState.ToString());
    }
}
