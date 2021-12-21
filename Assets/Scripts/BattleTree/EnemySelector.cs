using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySelector : MonoBehaviour
{
    int dir = -1;
    public Transform player;

    public Vector3 target;
    public float smoothTime = 0.3F;
    private Vector3 velocity = Vector3.zero;

    bool moving = false;

    // Start is called before the first frame update
    void Start()
    {
        SaveState save = (SaveState)FindObjectOfType(typeof(SaveState));
        player.position = save.enemySelector;
    }

    // Update is called once per frame
    void Update()
    {
        if ( Input.GetAxis("Horizontal") < 0 )
        {
            dir = -1;
            transform.localPosition = new Vector3( -.6f, .6f, 0 );
            transform.eulerAngles = new Vector3( 0, 0, 125 );
        }
        if ( Input.GetAxis("Horizontal") > 0 )
        {
            dir = 1;
            transform.localPosition = new Vector3( .6f, .6f, 0 );
            transform.eulerAngles = new Vector3( 0, 0, 45 );
        }

        if ( Input.GetButtonDown("Select") && moving == false )
        {
            moving = true;
            SaveState save = (SaveState)FindObjectOfType(typeof(SaveState));
            if ( dir == -1 )
            {
                target = player.position + new Vector3( -5, 0, 5 );
                save.enemySelector = target;
            }
            if ( dir == 1  )
            {
                target = player.position + new Vector3( 5, 0, 5 );
                save.enemySelector = target;
            }
        }

        if ( moving )
        {
            player.position = Vector3.SmoothDamp(player.position, target, ref velocity, smoothTime);
        }
    }
}
