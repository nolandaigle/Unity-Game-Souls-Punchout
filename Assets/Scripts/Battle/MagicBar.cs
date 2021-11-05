using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBar : MonoBehaviour
{
    public float speed = 1;
	private float xPos =  0f;
    int dir = 1;
    bool running = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ( running )
        {
            xPos += speed*dir*Time.deltaTime;
            if ( (dir == 1 && xPos > .5f) || (dir == -1 && xPos < -.5f ) )
            {
                dir = -dir;
            }
            transform.localPosition = new Vector3( xPos, transform.localPosition.y, transform.localPosition.z );
        }
    }

    public float GetBarVal()
    {
        running = false;
        return xPos;
    }

    public void StartBar()
    {
        xPos = 0;
        running = true;
    }

    public void StopBar()
    {
        running = false;
    }
}
