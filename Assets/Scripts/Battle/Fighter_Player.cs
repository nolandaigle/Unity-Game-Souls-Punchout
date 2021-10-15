using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter_Player : Fighter_Base
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
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

    	base.Update();
    }
}
