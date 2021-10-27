using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter_Soldier : Fighter_Base
{
	float aiTimer = 0;
	float aiTime = 1.5f;

    enum AIState { Blocking, Attacking }
    AIState aiState = AIState.Blocking;

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
            aiTimer += Time.deltaTime;
            if ( aiTimer > aiTime )
            {
                if ( aiState == AIState.Blocking )
                {
                    DefenseLogic();
                }
                else if ( aiState == AIState.Attacking )
                {
                    AttackLogic();
                }

                aiTimer = 0;
            }
        }

        base.Update();
    }

    void DefenseLogic()
    {
        if ( currentStamina >= rightHand.GetStaminaCost() )
        {
            if ( Random.Range(0, 10) > 4 )
            {
                RightHandAction();
                aiState = AIState.Attacking;
            }
            else
            {
                LeftHandAction(true);
            }
        }
        else
        {
            Stand();
        }
    }

    void AttackLogic()
    {
        if ( currentStamina >= rightHand.GetStaminaCost() )
        {
            if ( Random.Range(0, 10) > 4 )
            {
                RightHandAction();
            }
            else
            {
                LeftHandAction(true);
                aiState = AIState.Blocking;
            }
        }
        else
        {
            Stand();
        }
    }
}
