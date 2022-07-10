using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staff : Weapon
{
    public MagicBar magicBar;
    public Fighter_Base fighter;

    public float inputTimer = 0;
    bool running = false;

    public MeshFilter meshFilter;

    int spellPhase = 1;
    string currentSpell ="none";
    public string spell1 = "attack";
    public string spell2 = "heal";

    // Start is called before the first frame update
    void Start()
    {
        ResetColors();
    }

    // Update is called once per frame
    void Update()
    {
        if ( running )
        {
            inputTimer += Time.deltaTime;
            if ( Input.GetButtonDown("RightHandAction") && inputTimer > .1f )
            {
                print(spellPhase);
                inputTimer = 0;
                CastSpell(spellPhase);
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
            print("asdf");
            spellPhase = 1;
            running = false;
            magicBar.StopBar();
            ResetColors();
            inputTimer = 0;
        }
    }

    override public float GetDamage()
    {
        running = false;
        float dmg = Mathf.Abs(magicBar.GetBarVal()*10f);
        return dmg;
    }

    override public float GetHeal()
    {
        running = false;
        float dmg = Mathf.Abs(magicBar.GetBarVal()*10f);
        return dmg;
    }

    void CastSpell(int phase)
    {
        if ( phase == 1 )
        {
            if ( magicBar.GetBarVal() > 0 )
            {
                var mesh = meshFilter.mesh;
                Vector2[] uv = mesh.uv;
                Color[] colors = new Color[uv.Length];
                for (var i = 0; i< uv.Length; i++)
                    colors[i] = Color.Lerp(Color.red, Color.white, uv[i].x);
                mesh.colors = colors;
                currentSpell = spell1;
            }
            if ( magicBar.GetBarVal() < 0 )
            {
                var mesh = meshFilter.mesh;
                Vector2[] uv = mesh.uv;
                Color[] colors = new Color[uv.Length];
                for (var i = 0; i< uv.Length; i++)
                    colors[i] = Color.Lerp(Color.blue, Color.white, uv[i].x);
                mesh.colors = colors;
                currentSpell = spell2;
            }
            
            spellPhase = 2;
            magicBar.StartBar(-.5f);
        }
        else if (phase == 2 )
        {
            if ( currentSpell == "attack" )
                fighter.ChangeState("RightHandHit");
            if ( currentSpell == "heal" )
                fighter.Heal(GetHeal());
            spellPhase = 1;
            magicBar.StopBar();
            running = false;

            ResetColors();
            
        }
    }

    void ResetColors()
    {
        Color one = Color.green;
        Color two = Color.green;
        if ( spell1 == "attack" )
            one = Color.red;
        if ( spell2 == "heal" )
            two = Color.blue;
            
        var mesh = meshFilter.mesh;
        Vector2[] uv = mesh.uv;
        Color[] colors = new Color[uv.Length];
        for (var i = 0; i< uv.Length; i++)
            colors[i] = Color.Lerp(one, two, uv[i].x);
        mesh.colors = colors;
    }
}
