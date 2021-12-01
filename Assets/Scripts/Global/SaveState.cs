using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveState : MonoBehaviour {
    private static SaveState _instance;

    public static SaveState Instance { get { return _instance; } }

    public string playerClass = "hello!";


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }

    public void Start()
    {
        playerClass = "hello!";
    }

    public SaveState LoadFile()
    {
        string path = Application.dataPath+"/savefile.txt";
        try
        {
            using (StreamReader reader = new StreamReader(path))
            {
                string output = reader.ReadToEnd();
                reader.Close();
                File.Delete(path);
                return JsonUtility.FromJson<SaveState>(output);
            }
        }
        catch
        {
            print("Could not load file.");
            return null;
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