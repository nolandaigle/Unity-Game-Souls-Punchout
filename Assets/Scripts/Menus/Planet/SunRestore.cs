using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunRestore : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SaveState save = (SaveState)FindObjectOfType(typeof(SaveState));
        
        if ( save.currentPlanet == "Sun" )
        {
            save.playerCurrentHealth = save.playerMaxHealth;
            save.SaveFile();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
