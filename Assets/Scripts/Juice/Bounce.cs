using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{
    bool bouncing = true;

    // User Inputs
    public float amplitude = 0.5f;
    public float frequency = 1f;

    public float rotAmplitude = 1f;
    public float rotFrequency = 1f;
 
    // Position Storage Variables
    Vector3 posOffset = new Vector3 ();
    Vector3 tempPos = new Vector3 ();
    Vector3 rotOffset = new Vector3 ();
    Vector3 tempRot = new Vector3 ();
 
    // Use this for initialization
    void Start () {
        // Store the starting position & rotation of the object
        posOffset = transform.position;
        rotOffset = transform.rotation.eulerAngles;
    }
     
    // Update is called once per frame
    void Update () {
        if ( bouncing )
        {
            // Float up/down with a Sin()
            tempRot = rotOffset;
            tempRot.z += Mathf.Sin (Time.fixedTime * Mathf.PI * rotFrequency) * rotAmplitude;
    
            transform.rotation = Quaternion.Euler(tempRot);
            
            // Float up/down with a Sin()
            tempPos = posOffset;
            tempPos.y += Mathf.Sin (Time.fixedTime * Mathf.PI * frequency) * amplitude;
    
            transform.position = tempPos;
        }
    }

    void StopBounce() {
        bouncing = false;
    }

    void StartBounce(bool preserveY=true) {
        bouncing = true;
        // Store the starting position & rotation of the object
        
        if ( preserveY)
            posOffset = new Vector3( transform.position.x, posOffset.y, transform.position.z  );
        else
            posOffset = transform.position;
    }
}
