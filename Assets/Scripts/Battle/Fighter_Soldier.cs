using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter_Soldier : Fighter_Base
{
	float attackTimer = 0;
	float attackTime = 2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        attackTimer += Time.deltaTime;
        if ( attackTimer > attackTime )
        {
        	RightHandAction();
        	attackTimer = 0;
        }

        base.Update();
    }
}
