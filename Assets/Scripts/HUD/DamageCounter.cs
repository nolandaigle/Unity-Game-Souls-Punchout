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

	int rollNums = 2;
	int currentRollNum = 0;

    private float smoothTime = 0.1f;
    private float velocity = 0f;
	private float floatNumber =  0;
	private float displayNum = 0;

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
        	floatNumber = Mathf.SmoothDamp(floatNumber, finalNumber, ref velocity, smoothTime);
			currentNumber = Mathf.RoundToInt(floatNumber);

			displayNum = (currentNumber - (( rollNums - currentRollNum )*range.y));

			if ( displayNum > range.y )
			{
				displayNum = 1;
				currentRollNum -= 1;
			}
			if ( currentNumber == finalNumber )
			{
				aSource.clip = doneSound;
				aSource.Play();
				rolling = false;
			}
			else
				aSource.Play();
			text.text = ""+displayNum;
        }
    }

    public void Roll( int fNum, float time )
    {
		smoothTime = time;
		currentNumber = (int)range.x;
		currentRollNum = rollNums;
		floatNumber = currentNumber;
    	finalNumber = fNum+((int)range.y*currentRollNum);
    	rolling = true;
    	aSource.clip = rollingSound;
    }

    public void Cancel() {
    	rolling = false;
    }
}
