using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldPlayer : MonoBehaviour
{
	public Animator anim;
	public CharacterController cc;

    // Start is called before the first frame update
    void Start()
    {
        anim.Play("Walk");
    }

    // Update is called once per frame
    void Update()
    {
    	Vector3 direction = Vector3.zero;

        direction.x = Input.GetAxis("Horizontal");
        direction.y = Input.GetAxis("Vertical");

        anim.SetFloat("speed", direction.magnitude);

        cc.Move(direction);
    }
}
