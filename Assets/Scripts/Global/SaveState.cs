using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveState : MonoBehaviour {
    private static SaveState _instance;

    public static SaveState Instance { get { return _instance; } }

    public string playerClass = "nodata";
    public string currentEnemy = "nodata";

    public float playerMaxHealth = 10;
    public float playerCurrentHealth = 10;

    public Vector3 enemySelector = Vector3.zero;

    public int currentEnemyID;

    public int charUnlock = 1;

    public bool boss = false;

    public int level = 1;

    public bool dead = false;

    public int[] tree = null;

    public string astrology = "sagittarius";


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
        DontDestroyOnLoad(gameObject);

        LoadFile();
        tree = null;
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
                JsonUtility.FromJsonOverwrite( reader.ReadToEnd(), this );

                reader.Close();

                //Load variables

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

    public void Reset()
    {
        playerCurrentHealth = playerMaxHealth;
        enemySelector = Vector3.zero;
        playerClass = "nodata";
        currentEnemy = "nodata";
        currentEnemyID = -1;
        boss = false;
        level = 1;
        dead = false;
        tree = null;

        string[] astrologyChoices = {"aries", "sagittarius", "leo", "virgo", "taurus", "capricorn", "libra", "aquarius", "gemini", "pisces", "cancer", "scorpio"};
        astrology = astrologyChoices[Random.Range(0, astrologyChoices.Length)];
    }
}