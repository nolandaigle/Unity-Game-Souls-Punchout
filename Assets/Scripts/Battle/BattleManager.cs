using System.Collections;
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

    // Start is called before the first frame update
    void Start()
    {
        foreach ( Prefabs i in EnemyPrefabs )
        {
            enemyDictionary.Add( i.name, i.prefab );
        }

        LoadEnemy("BirdBaby");
    }

    // Update is called once per frame
    void Update()
    {
        if ( battleOver == true )
        {
            endTimer += Time.deltaTime;
            if ( endTimer > endTime )
            {
                SceneManager.LoadScene("Overworld");
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
