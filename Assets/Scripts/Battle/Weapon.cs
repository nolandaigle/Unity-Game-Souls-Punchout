using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Animator anim;
    public AudioClip blockSound;
    public float chargeTime = .75f;
    public float hitRecover = .5f;
    public string type = "sword";
    public string damageType = "physical";

    public float effectMin = 1;
    public float effectMax = 6;

    public float staminaCost = 5;

    public bool player = false;

    SaveState save;

    // Start is called before the first frame update
    void Start()
    {
        save = (SaveState)FindObjectOfType(typeof(SaveState));

        if ( save.astrology == "aries" )
        {
            staminaCost = staminaCost*.75f;
        }
        else if ( save.astrology == "capricorn" )
        {
            effectMin += 1;
            chargeTime += .25f;
        }
        else if ( save.astrology == "virgo" )
        {
            effectMin += 1;
            effectMax -= 1;
        }
        else if ( save.astrology == "taurus" )
        {
            effectMax -= 1.5f;
        }
        else if ( save.astrology == "cancer" )
        {
            effectMax += 2f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    virtual public string GetDamageType()
    {
        return damageType;
    }

    virtual public float GetDamage()
    {
    	return Random.Range(effectMin, effectMax);
    }

    virtual public float GetHeal()
    {
    	return Random.Range(effectMin, effectMax);
    }

    public float GetStaminaCost()
    {
    	return staminaCost;
    }

    public string GetWeaponType()
    {
        return type;
    }

    virtual public void ChangeState(string state)
    {
        anim.Play(state);
    }
}
