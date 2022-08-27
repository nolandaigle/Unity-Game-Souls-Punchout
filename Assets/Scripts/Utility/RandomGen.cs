using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomGen : MonoBehaviour
{
    public GameObject[] prefabs;
    public string[] names;
    private int selected = 0;

    // Start is called before the first frame update
    void Start()
    {
        selected = Random.Range(0, prefabs.Length );
        Instantiate( prefabs[selected], transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string GetSelected()
    {
        if ( names.Length > 0 )
            return names[selected];
        else
            return "none";
    }
}
