﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDBar : MonoBehaviour
{
	public Fighter_Base fighter;
	public Transform statBar;
	public Transform actualStat;

    public string stat = "health";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ( stat == "health")
            actualStat.localScale = new Vector3( ( ((float)fighter.GetCurrentHealth()/(float)fighter.GetMaxHealth())*statBar.localScale.x )-(.1f*(float)fighter.GetCurrentHealth()), statBar.localScale.y-.1f, statBar.localScale.z );
        else if ( stat == "stamina")
            actualStat.localScale = new Vector3( ( ((float)fighter.GetCurrentStamina()/(float)fighter.GetMaxStamina())*statBar.localScale.x ) - (.1f*(float)fighter.GetCurrentStamina()), statBar.localScale.y-.1f, statBar.localScale.z );
    }
}