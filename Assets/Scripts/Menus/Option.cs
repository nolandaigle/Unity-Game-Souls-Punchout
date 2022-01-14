using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Option : MonoBehaviour
{
	public AudioSource aSource;
	public AudioClip selector;

    public string[] optionsText;
    public GameObject textObject;
    public float spacing = 1;
    GameObject[] optionsMesh;

    int selected = 0;

    bool buttonReset = true;
    // Start is called before the first frame update
    void Start()
    {
        optionsMesh = new GameObject[optionsText.Length];
        for ( int i = 0; i < optionsMesh.Length; i++ )
        {
            optionsMesh[i] = Instantiate(textObject, transform.position, Quaternion.identity);
            optionsMesh[i].GetComponent<TextMeshPro>().text = optionsText[i];
            optionsMesh[i].GetComponent<TextMeshPro>().transform.position += new Vector3( 0, -i*spacing, 0 );
        }
        optionsMesh[selected].GetComponent<TextMeshPro>().color = Color.yellow;
    }

    // Update is called once per frame
    void Update()
    {
        if ( Input.GetAxis("Vertical") > 0 )
        {
            if ( selected > 0 && buttonReset == true )
            {
                Select(selected-1);
            }
        }
        else if ( Input.GetAxis("Vertical") < 0 )
        {
            if ( selected < optionsMesh.Length-1 && buttonReset == true )
            {
                Select(selected+1);
            }
        }
        else if ( Input.GetAxis("Vertical") < .15 && Input.GetAxis("Vertical") >-.15 )
            buttonReset = true;
    }

    void Select(int select)
    {
    	aSource.clip = selector;
    	aSource.Play();

        optionsMesh[selected].GetComponent<TextMeshPro>().color = new Color(200,200,200);
        if ( optionsMesh[selected].GetComponent<Bounce>() )
	        optionsMesh[selected].GetComponent<Bounce>().enabled = false;
        selected = select;
        optionsMesh[selected].GetComponent<TextMeshPro>().color = Color.yellow;
        if ( optionsMesh[selected].GetComponent<Bounce>() )
	        optionsMesh[selected].GetComponent<Bounce>().enabled = true;
        buttonReset = false;
    }

    public void Disable()
    {
    	for ( int i = 0; i < optionsMesh.Length; i++ )
        {
            optionsMesh[i].SetActive(false);
        }
    }
    public void Enable()
    {
    	if ( optionsMesh != null )
    	{
	    	for ( int i = 0; i < optionsMesh.Length; i++ )
	        {
	            optionsMesh[i].SetActive(true);
	        }
    	}
    }

    public string GetSelected()
    {
        return optionsText[selected];
    }
}
