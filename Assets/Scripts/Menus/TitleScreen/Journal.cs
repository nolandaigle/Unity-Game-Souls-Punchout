using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Journal : MonoBehaviour
{
	public TitleScreen title;
	public Option options;
	public Option journalOptions;
	public TextRead text;

	public Transform background;

	string currentSelection = "";

    // Start is called before the first frame update
    void Start()
    {
        text.gameObject.SetActive(true);
        text.SetText("");
    }

    // Update is called once per frame
    void Update()
    {
        if ( Input.GetButtonDown("Select") )
        {
            StartCoroutine(waiter());
        }
        if ( Input.GetButtonDown("Cancel") )
        {
        	journalOptions.Disable();
        	text.gameObject.SetActive(false);
        	options.Enable();
        	title.gameObject.SetActive(true);
        	background.gameObject.SetActive(true);
        	gameObject.SetActive(false);
        }
    }

    IEnumerator waiter()
    {
        yield return new WaitForSeconds(.1f);
        if ( currentSelection != journalOptions.GetSelected() )
        {
	    	if ( text.gameObject.active == false )
	    		text.gameObject.SetActive(true);

            if ( journalOptions.GetSelected() == "Soldier 1" )
        	{
        		text.SetText("Even  though  they're  trying  to  kill  me,  I  can't  help  but  feel  like  they're  a  nice  person...\n\n Seems  like  the  best  way  to  fight  them  is  with  a  nice  sword  thrust  to  the  face.");
        	}
        	else if ( journalOptions.GetSelected() == "Soldier 2" )
        	{
        		text.SetText("Very  aggressive.  I  find  the  best  way  to  get  them  is  by  blocking  their  attacks.  After  a  block,  they  seem  to  lose  their  stamina.\n\n Then,  I  like  to  hit  them  with  a  nice  sword  thrust  to  the  face.");
        	}
        	else if ( journalOptions.GetSelected() == "Knight" )
        	{
        		text.SetText("Kind  of  a  jerk.  Their  sword  hurts  real  bad.  But  I've  noticed  I  can  interrupt  their  sword  attack  with  a  nice  sword  thrust  to  the  face.\n\n It  seems,  in  this  dream,  that  most  problems  can  be  solved  with  a  nice  sword  thrust  to  the  face.");
        	}
            else if ( journalOptions.GetSelected() == "Witch" )
            {
                text.SetText("They  don't  talk  much.  Their  magic  attacks  bust  right  through  my  shield...  Kinda  reminds  me  of  my  mom?");
            }
    	}
    }
}
