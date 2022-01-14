using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TextRead : MonoBehaviour
{
	public TextMeshPro textmesh;
	string text;

	public float textTime = .2f;
	float textTimer = 0;
	float ogTextTime;

	int currentChar = 0;

	bool done = false;


    public AudioSource aSource;
    public AudioClip blip;
    public AudioClip end;

    public string nextScene = "";

    // Start is called before the first frame update
    void Start()
    {
        text = textmesh.text;

        aSource.clip = blip;

        ogTextTime = textTime;
    }

    // Update is called once per frame
    void Update()
    {
    	if ( !done )
    	{
	        textTimer += Time.deltaTime;

	    	if ( Input.GetButtonDown("Select") )
	    	{
	    		done = true;
				textmesh.text = text;
	    	}
	        else if ( textTimer > textTime )
	        {
	        	textTimer = 0;
	        	currentChar += 1;

	        	if ( currentChar > text.Length )
	        		done = true;
	        	else
					textmesh.text = text.Substring(0, currentChar);

				if ( text.Substring(currentChar-1, 1) == "." )
					textTime = 1;
				else if ( text.Substring(currentChar-1, 1) != " " )
				{
					textTime = ogTextTime;
					aSource.Play();
				}
				else
					textTime = ogTextTime;
	        }
    	}
    	else
    	{

	    	if ( nextScene != "" && Input.GetButtonDown("Select") )
	    	{
	    		aSource.clip = end;
	    		aSource.Play();

	    		textmesh.text = "";

            	StartCoroutine(waiter());
	    	}
    	}
    }

    public void SetText(string newText)
    {
    	text = newText;
    	currentChar = 0;
    	done = false;
    	textmesh.text = "";
    }

    IEnumerator waiter()
    {
            yield return new WaitForSeconds(.5f);
            SceneManager.LoadScene(nextScene);
    }
}
