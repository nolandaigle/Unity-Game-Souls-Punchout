using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionalDestroy : MonoBehaviour
{
	public string condition = "";
	public string value = "";

	private SaveState save;
    // Start is called before the first frame update
    void Start()
    {
        save = (SaveState)FindObjectOfType(typeof(SaveState));
        switch (condition) {
        	case "astrology":
        		if (save.astrology != value)
        			Destroy(this.gameObject);
        		break;
            case "currentPlanet":
                if (save.currentPlanet != value)
        			Destroy(this.gameObject);
                save.activePlanet = save.currentPlanet;
                save.SaveFile();
        		break;

        }
        // string conditionBool = save.GetType().GetProperty(condition);

        // if ( conditionBool == null || conditionBool != value )
        // {
        // 	Destroy(this.gameObject);
        // }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
