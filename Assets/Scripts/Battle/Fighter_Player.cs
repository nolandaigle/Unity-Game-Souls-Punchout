using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter_Player : Fighter_Base
{
    SaveState save;

    // Start is called before the first frame update
    override protected void Start()
    {
        save = (SaveState)FindObjectOfType(typeof(SaveState));

        //Astrology/Planetary Stuff
        if ( save.astrology == "sagittarius" )
        {
            dodgeCost = 4;
            dodgeTime = .35f;
            hurtRecover = 2f;
        }
        else if ( save.astrology == "leo" )
        {
            staminaTime = .4f;
            hurtRecover = 2.5f;
        }
        else if ( save.astrology == "aries" )
        {
            hurtRecover = 2.25f;
        }
        else if ( save.astrology == "capricorn" )
        {
            damageScale = 1.2f;
        }
        else if ( save.astrology == "virgo" )
        {
            damageScale = .8f;
            dodgeCost = 4.5f;
        }
        else if ( save.astrology == "cancer" )
        {
            damageScale = 1.4f;
        }

        //

        base.Start();

        if ( save.playerCurrentHealth != null)
            currentHealth = save.playerCurrentHealth;
        if ( save.playerMaxHealth != null)
            maxHealth = save.playerMaxHealth;
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

    override public void Die()
    {
        ChangeState(State.Dead);
        bm.EndBattle(true);
    }

    override public void StopFighting()
    {
        save.playerCurrentHealth = currentHealth;
        base.StopFighting();
    }
}
