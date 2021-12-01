using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter_Birdboss : Fighter_Base
{
	float aiTimer = 0;
	float aiTime = 1.75f;
    
    float deathAnimationTime = 6;
    float deathAnimationTimer = 0;

    enum AIState { Blocking, Attacking }
    AIState aiState = AIState.Attacking;

    public GameObject nextPhase;

    // Start is called before the first frame update
    override protected void Start()
    {
        base.Start();
        bm.stages = 2;
    }

    // Update is called once per frame
    override protected void Update()
    {
        if ( currentState != State.Dead )
        {
            aiTimer += Time.deltaTime;
            if ( aiTimer > aiTime )
            {
                if ( aiState == AIState.Attacking )
                {
                    AttackLogic();
                }

                aiTimer = 0;
            }
        }
        else
        {
            deathAnimationTimer += Time.deltaTime;
            if ( deathAnimationTimer > deathAnimationTime )
            {
                bm.LoadEnemy("BirdBaby");
                Destroy(transform.gameObject);
            }
        }

        base.Update();
    }

    void AttackLogic()
    {
        
        if ( Random.Range( 0, 10 ) > 7 )
        {
            if ( currentStamina >= rightHand.GetStaminaCost() )
            {
                RightHandAction();
            }
            else
            {
                Stand();
            }
        }
        else
        {
            if ( currentStamina >= leftHand.GetStaminaCost() )
            {
                    LeftHandAction();
            }
            else
            {
                Stand();
            }
        }
    }
}
