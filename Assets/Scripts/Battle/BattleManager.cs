using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour
{
    float endTimer = 0;
    public float endTime = 3;

    bool battleOver = false;

    // Start is called before the first frame update
    void Start()
    {
        
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
        battleOver = true;
        BroadcastMessage("StopFighting");
    }
}
