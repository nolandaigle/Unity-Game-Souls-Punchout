using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TreeEnemy : MonoBehaviour
{
    public string battleName = "Soldier";
    public Transform[] enemyOptions;

    public int rank;

    public int objectID;

    public bool boss = false;

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
        if ( other.gameObject.tag == "Player")
        {
            print("This: "+gameObject.name+" You: "+other.gameObject.name);
            SaveState save = (SaveState)FindObjectOfType(typeof(SaveState));
            save.currentEnemyID = objectID;
            save.currentEnemy = battleName;
            save.SaveFile();
            SceneManager.LoadScene("Battle");
        }
    }
}
