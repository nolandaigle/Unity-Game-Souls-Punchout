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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    virtual public int GetDamage()
    {
    	return Random.Range(1, 6);
    }

    public int GetStaminaCost()
    {
    	return 5;
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
