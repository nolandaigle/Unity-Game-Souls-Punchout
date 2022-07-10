using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    public AudioSource aSource;

	bool fade = false;
	public Material screenFade;
    float a = 0;
    bool fading = false;

    public string nextScene;
    public float fadeSpeed = 3;

    // Start is called before the first frame update
    void Start()
    {
        screenFade.color = new Color( 0, 0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
    	if ( Input.anyKeyDown )
    		fading = true;
    		
        if ( fading )
        {
        	a += Time.deltaTime/fadeSpeed;
        	if ( a >= 1 )
        	{
        		screenFade.color = new Color( 0, 0, 0, 1);

                SceneManager.LoadScene(nextScene);
        	}
        	else
        	{
        		aSource.volume = 1-a;
        		screenFade.color = new Color( 0, 0, 0, a);
        	}
        }
    }
}
