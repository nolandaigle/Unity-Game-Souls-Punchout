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

    public int effectMin = 1;
    public int effectMax = 6;

    public int staminaCost = 5;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    virtual public string GetDamageType()
    {
        return damageType;
    }

    virtual public int GetDamage()
    {
    	return Random.Range(effectMin, effectMax);
    }

    virtual public int GetHeal()
    {
    	return Random.Range(effectMin, effectMax);
    }

    public int GetStaminaCost()
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
