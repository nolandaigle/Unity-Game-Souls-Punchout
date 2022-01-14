using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeIn : MonoBehaviour
{
    public Material screenFade;
    float a = 1;
    bool fading = true;

    // Start is called before the first frame update
    void Start()
    {
        screenFade.color = new Color( 0, 0, 0, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if ( fading )
        {
        	a -= Time.deltaTime/3;
        	if ( a <= 0 )
        	{
        		a = 0;
        		fading = false;
        	}
        	else
        	{
        		screenFade.color = new Color( 0, 0, 0, a);
        	}
        }
    }
}
