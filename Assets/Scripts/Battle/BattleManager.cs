﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class BattleManager : MonoBehaviour
{
    float endTimer = 0;
    public float endTime = 3;

    bool battleOver = false;

    [Serializable]
    public struct Prefabs {
        public string name;
        public GameObject prefab;
    }
    public Prefabs[] EnemyPrefabs;
    Dictionary<string, GameObject> enemyDictionary = new Dictionary<string, GameObject>();

    public Fighter_Base player;

    public int stages = 1;

    public SaveState save;

    // Start is called before the first frame update
    void Start()
    {
        foreach ( Prefabs i in EnemyPrefabs )
        {
            enemyDictionary.Add( i.name, i.prefab );
        }
        
        save = (SaveState)FindObjectOfType(typeof(SaveState));
        string e = save.currentEnemy;
        
        LoadEnemy(e);
    }

    // Update is called once per frame
    void Update()
    {
        if ( battleOver == true )
        {
            endTimer += Time.deltaTime;
            if ( endTimer > endTime )
            {
                // SceneManager.LoadScene("Overworld");
                if ( save.boss == false )
                {
                    if ( save.level == 1 )
                        SceneManager.LoadScene("BattleTree");
                    else if ( save.level == 2 )
                        SceneManager.LoadScene("BattleTree-Level2");                    
                }
                else
                {
                    save.enemySelector = new Vector3(0,0,0);
                    if ( save.level == 1 )
                    {
                        save.level = 2;
                        SceneManager.LoadScene("BattleTree-Level2");                    
                    }
                }
            }
        }
    }

    public void EndBattle()
    {
        if ( stages == 1 )
        {
            battleOver = true;
            BroadcastMessage("StopFighting");
        }
        else
        {
            stages -= 1;
        }
    }

    public void LoadEnemy(string name )
    {
        GameObject temp = Instantiate( enemyDictionary[name], transform.position, Quaternion.identity);
        temp.GetComponent<Fighter_Base>().enemy = player;
        temp.GetComponent<Fighter_Base>().bm = this;
        player.enemy = temp.GetComponent<Fighter_Base>();
    }
}
