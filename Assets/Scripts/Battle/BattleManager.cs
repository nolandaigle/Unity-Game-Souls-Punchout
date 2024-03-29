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
    bool playerDead = false;

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
                if ( playerDead == true )
                {
                    SceneManager.LoadScene("TitleScreen");
                }            
                // SceneManager.LoadScene("Overworld");
                else if ( save.boss == false )
                {
                        SceneManager.LoadScene("Planet");                 
                }
                else
                {
                    save.enemySelector = new Vector3(0,0,0);
                    if ( save.level == 1 )
                    {
                        save.tree = null;
                        save.level = 2;
                        save.playerCurrentHealth = save.playerMaxHealth;
                        save.boss = false;
                        save.SaveFile();
                        SceneManager.LoadScene("BattleTree-Level2");                    
                    }
                    else if ( save.level == 2 )
                    {
                        save.tree = null;
                        save.level = 3;
                        save.playerCurrentHealth = save.playerMaxHealth;
                        save.boss = false;
                        save.SaveFile();
                        SceneManager.LoadScene("BattleTree-Level3");
                    }
                }
            }
        }
    }

    public void EndBattle(bool dead = false)
    {
        if ( stages == 1 )
        {
            battleOver = true;
            if ( dead )
            {
                playerDead = true;
                save.dead = true;
                save.SaveFile();
            }
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
