using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDBar : MonoBehaviour
{
	public Fighter_Base fighter;
    public OverworldPlayer player;
	public Transform statBar;
	public Transform actualStat;

    public string stat = "health";

    public Color negativeColor;
    public Color depletedColor;
    public Color halfColor;
    public Color fullColor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ( fighter )
        {
            if ( stat == "health")
            {
                actualStat.localScale = new Vector3( ( ((float)fighter.GetCurrentHealth()/(float)fighter.GetMaxHealth())*statBar.localScale.x )-(.01f*(float)fighter.GetCurrentHealth()), statBar.localScale.y-.1f, statBar.localScale.z );
            }
            else if ( stat == "stamina")
            {
                if ( fighter.GetCurrentStamina() < 0 )
                    actualStat.GetComponent<Renderer>().material.color = negativeColor;
                else if ( fighter.GetCurrentStamina() < fighter.rightHand.staminaCost &&  fighter.GetCurrentStamina() < fighter.dodgeCost )
                    actualStat.GetComponent<Renderer>().material.color = depletedColor;
                else if (  ( fighter.GetCurrentStamina() > fighter.rightHand.staminaCost &&  fighter.GetCurrentStamina() < fighter.dodgeCost ) || ( fighter.GetCurrentStamina() < fighter.rightHand.staminaCost &&  fighter.GetCurrentStamina() > fighter.dodgeCost ) )
                    actualStat.GetComponent<Renderer>().material.color = halfColor;
                else
                    actualStat.GetComponent<Renderer>().material.color = fullColor;

                actualStat.localScale = new Vector3( ( ((float)fighter.GetCurrentStamina()/(float)fighter.GetMaxStamina())*statBar.localScale.x ) - (.01f*(float)fighter.GetCurrentStamina()), statBar.localScale.y-.1f, statBar.localScale.z );
            }
        }
        else
        {
            if ( stat == "health")
            {
                actualStat.localScale = new Vector3( ( ((float)player.GetCurrentHealth()/(float)player.GetMaxHealth())*statBar.localScale.x )-(.01f*(float)player.GetCurrentHealth()), statBar.localScale.y-.1f, statBar.localScale.z );
            }
        }
    }
}
