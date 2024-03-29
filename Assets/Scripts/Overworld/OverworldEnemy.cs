﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OverworldEnemy : MonoBehaviour
{
    public Animator anim;
    public SpriteRenderer sprite;

    public float speed = 5;
    Vector3 moveDir = Vector3.zero;

    public string battleName = "Soldier";

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnTriggerEnter(Collider other)
    {
        SaveState save = (SaveState)FindObjectOfType(typeof(SaveState));
        save.currentEnemy = battleName;
        // other.transform.GetComponent<OverworldPlayer>().StartBattle();
        SceneManager.LoadScene("Battle");
    }
}
