using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveState : MonoBehaviour {
    private static SaveState _instance;

    public static SaveState Instance { get { return _instance; } }

    public string playerClass = "nodata";
    public string currentEnemy = "nodata";

    public int playerMaxHealth = 10;
    public int playerCurrentHealth = 10;


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public void Start()
    {
    }

    public string LoadFile()
    {
        string path = Application.dataPath+"/savefile.txt";
        try
        {
            using (StreamReader reader = new StreamReader(path))
            {
                SaveState output = JsonUtility.FromJson<SaveState>( reader.ReadToEnd() );

                reader.Close();
                File.Delete(path);

                //Load variables
                playerClass = output.playerClass;
                currentEnemy = output.currentEnemy;

                return "Success!";
            }
        }
        catch
        {
            print("Could not load file.");
            return "Failure :(";
        }
    }

    public void SaveFile()
    {
        string path = Application.dataPath+"/savefile.txt";
        try
        {
            using (StreamReader reader = new StreamReader(path))
            {
                print(reader.ReadToEnd());
                reader.Close();
                File.Delete(path);
                StreamWriter writer = new StreamWriter(path, true);
                writer.Write(JsonUtility.ToJson(this));
                writer.Close();
            }
        }
        catch
        {
            //If none exists, create a file
            StreamWriter writer = new StreamWriter(path, true);
            writer.Write(JsonUtility.ToJson(this));
            writer.Close();
        }
    }
}