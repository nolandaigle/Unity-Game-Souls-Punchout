using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldPlayer : MonoBehaviour
{
	public Animator anim;
	public CharacterController cc;
    public SpriteRenderer sprite;

    public float speed = 5;
    Vector3 moveDir = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        anim.Play("Walk");
    }

    // Update is called once per frame
    void Update()
    {
        Control();

        Movement();
    }

    void Control()
    {
        moveDir.x = Input.GetAxis("Horizontal");
        moveDir.y = Input.GetAxis("Vertical");

    }

    void Movement()
    {
        if ( moveDir.x < 0 )
            sprite.flipX = true;
        else if ( moveDir.x > 0 )
            sprite.flipX = false;

        anim.SetFloat("speed", moveDir.magnitude);

        cc.Move(moveDir*speed*Time.deltaTime);
    }
}
