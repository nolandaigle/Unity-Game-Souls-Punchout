using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    public Option options;

    public Material screenFade;
    float a = 0;
    bool fading = false;

    public AudioSource aSource;
    public AudioSource gameStartAudio;

    public Transform journal;
    public Transform background;

    public Transform cam;

    private SaveState save;

    // Start is called before the first frame update
    void Start()
    {

        save = (SaveState)FindObjectOfType(typeof(SaveState));
        save.LoadFile();

        screenFade.color = new Color( 0, 0, 0, 0);

        if ( save.dead == true )
        {
            cam.eulerAngles = new Vector3( 0, -180, 0 );
        }
        else
        {
            cam.eulerAngles = new Vector3( 0, 0, 0 );
        }
    }

    // Update is called once per frame
    void Update()
    {

        if ( fading )
        {
        	a += Time.deltaTime/6;
        	if ( a >= 1 )
        	{
        		a = 1;
                if ( save.dead )
                {
                    save.Reset();
                    save.SaveFile();
                    SceneManager.LoadScene("Intro");
                }
                else
                {
                    if ( save.level == 1 )
                    SceneManager.LoadScene("BattleTree");
                    else if ( save.level == 2 )
                        SceneManager.LoadScene("BattleTree-Level2");
                }
        	}
        	else
        	{
        		aSource.volume = 1-a;
        		screenFade.color = new Color( 0, 0, 0, a);
        	}
        }
        else
        {
        	if ( Input.GetButtonDown("Select") )
	        {
	            if ( options.GetSelected() == "Start" )
	            {
	            	gameStartAudio.Play();
	        		fading = true;
	            }
	            else if ( options.GetSelected() == "Journal" )
	            {
	            	options.Disable();
	            	journal.gameObject.SetActive(true);
	            	journal.GetComponent<Option>().Enable();
	            	background.gameObject.SetActive(false);
	            	transform.gameObject.SetActive(false);

	            }
	        }
        }
    }
}
