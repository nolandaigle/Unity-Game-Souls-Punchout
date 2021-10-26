using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public AudioClip blockSound;
    public float chargeTime = .75f;
    public float hitRecover = .5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetDamage()
    {
    	return Random.Range(1, 6);
    }

    public int GetStaminaCost()
    {
    	return 5;
    }
}
