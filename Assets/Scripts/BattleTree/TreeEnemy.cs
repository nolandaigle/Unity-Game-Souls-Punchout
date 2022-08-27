using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TreeEnemy : MonoBehaviour
{
    public RandomGen planetGen;
    
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
            SaveState save = (SaveState)FindObjectOfType(typeof(SaveState));
            save.currentEnemyID = objectID;
            save.currentEnemy = battleName;
            if ( planetGen )
                save.currentPlanet = planetGen.GetSelected();
            else
                save.currentPlanet = "none";
            save.SaveFile();
            SceneManager.LoadScene("Battle");
        }
    }
}
