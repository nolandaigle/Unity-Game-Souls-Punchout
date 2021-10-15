using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCounter : MonoBehaviour
{
	public AudioSource aSource;
	public AudioClip rollingSound;
	public AudioClip doneSound;

	public TextMesh text;

	int finalNumber = 0;
	int currentNumber;
	bool rolling = false;

	float rollTimer = 0;
	float rollTime = 1;

	float speedTimer = 0;
	float rollSpeed = 200;

	public Vector2 range = new Vector2( 1, 6 );

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ( rolling )
        {
        	speedTimer += Time.deltaTime;
        	if ( speedTimer*rollSpeed > 1 )
        	{
        		currentNumber += 1;
        		if ( currentNumber > range.y )
        		{
        			currentNumber = (int)range.x;
        		}
        		rollTimer += speedTimer;
        		if ( rollTimer > rollTime && currentNumber == finalNumber )
        		{
        			aSource.clip = doneSound;
        			aSource.Play();
        			rolling = false;
        		}
        		else
        			aSource.Play();

        		speedTimer = 0;
        		text.text = ""+currentNumber;
        	}
        }
    }

    public void Roll( int fNum, float time)
    {
    	rollTime = time;
    	finalNumber = fNum;
    	rolling = true;
    	aSource.clip = rollingSound;
    }

    public void Cancel() {
    	rolling = false;
    }
}
