﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter_Hammer : Fighter_Base
{
	float aiTimer = 0;
	public float aiTime = 1.5f;

    enum AIState { Attacking }
    AIState aiState = AIState.Attacking;

    // Start is called before the first frame update
    override protected void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    override protected void Update()
    {
        if ( currentState == State.Standing )
        {
            aiTimer += Time.deltaTime;
            if ( aiTimer > aiTime )
            {
                    AttackLogic();
            }
        }

        base.Update();
    }

    void AttackLogic()
    {
        aiTimer = 0;
        if ( currentStamina >= rightHand.GetStaminaCost() )
        {
            if ( Random.Range(0, 10) > 5 )
            {
                RightHandAction();
            }
        }
    }
}
