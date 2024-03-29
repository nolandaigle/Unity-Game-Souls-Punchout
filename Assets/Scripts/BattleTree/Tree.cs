﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    Node[] nodes;
    public GameObject[] nodePrefabs;
    public GameObject boss;

    Dictionary<int, List<GameObject> > rankedPrefabs = new Dictionary< int, List<GameObject> >();

    // Start is called before the first frame update
    void Start()
    {
        Generate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Generate()
    {

        SaveState save = (SaveState)FindObjectOfType(typeof(SaveState));
        int lastEnemyID = save.currentEnemyID;

        for ( int i = 0; i < nodePrefabs.Length; i++ )
        {
            if ( rankedPrefabs.ContainsKey(nodePrefabs[i].GetComponent<TreeEnemy>().rank ) )
            {
                print("1"+nodePrefabs[i]);
                rankedPrefabs[nodePrefabs[i].GetComponent<TreeEnemy>().rank].Add(nodePrefabs[i] );
            }
            else
            {
                print("2"+nodePrefabs[i]);
                rankedPrefabs.Add( nodePrefabs[i].GetComponent<TreeEnemy>().rank, new List<GameObject>() );
                rankedPrefabs[nodePrefabs[i].GetComponent<TreeEnemy>().rank].Add(nodePrefabs[i] );
            }
        }

        if ( save.enemySelector.z < rankedPrefabs.Count*5 )
        {

            float zPos = 5;
            int currentRow = 0;
            int enemyID = 1;
            
            bool newTree = false;
            int current = 0;
            if ( save.tree == null )
            {
            	newTree = true;
            	save.tree = new int[10];
        	}

            for ( int i = 0; i < rankedPrefabs.Count; i++ )
            {
                for ( int j = 0; j < 2+currentRow; j++ )
                {
                    if ( enemyID != lastEnemyID && save.enemySelector.z < zPos )
                    {
                    	if ( newTree )
                    	{
                            print("new");
                    		save.tree[current] = Random.Range(0, rankedPrefabs[i].Count);
                    	}
                        int temp_x = 10*j - ( (1+currentRow)*10/2 );
                        print(i);
                        print(current);
                        GameObject enemy = Instantiate(rankedPrefabs[i][save.tree[current]], transform.position + new Vector3(temp_x, zPos/2, zPos), Quaternion.identity );
                        enemy.GetComponent<TreeEnemy>().objectID = enemyID;
                        enemy.GetComponent<TreeEnemy>().planetGen.seed = (int)Mathf.Round(enemy.transform.position.x);
                    }
                    enemyID += 1;
                    current += 1;
                }
                zPos += 5;
                currentRow += 1;
            }
        }
        else
        {
            save.boss = boss;
            GameObject enemy = Instantiate(boss, save.enemySelector + new Vector3(0, 5/2, 5), Quaternion.identity );
        }
    }
}
