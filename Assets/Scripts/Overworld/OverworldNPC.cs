using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldNPC : MonoBehaviour
{
    bool playerInRange = false;
    public GameObject selector;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter( Collider other )
    {
        if ( other.tag == "Player" )
        {
            playerInRange = true;
            selector.SetActive(true);
        }
    }

    void OnTriggerExit( Collider other )
    {
        if ( other.tag == "Player" )
        {
            playerInRange = false;
            selector.SetActive(false);
        }
    }
}
