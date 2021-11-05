using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staff : Weapon
{
    public MagicBar magicBar;
    public Fighter_Base fighter;

    public float inputTimer = 0;
    bool running;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if ( running )
        {
            inputTimer += Time.deltaTime;
            if ( Input.GetButtonDown("RightHandAction") && inputTimer > .1f )
            {
                inputTimer = 0;
                fighter.ChangeState("RightHandHit");
            }
        }
    }

    override public void ChangeState(string state)
    {
        base.ChangeState(state);
        if ( state == "RightHandPrep" )
        {
            running = true;
            magicBar.StartBar();
        }
        else
        {
            running = false;
            magicBar.StopBar();
        }
    }

    override public int GetDamage()
    {
        running = false;
        int dmg = Mathf.RoundToInt(Mathf.Abs(magicBar.GetBarVal()*10f));
        print(dmg);
        return dmg;
    }
}
