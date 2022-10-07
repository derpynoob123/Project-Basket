using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public sealed class DataPersistence : MonoBehaviour
{
    static DataPersistence singleton;
    public int bestScore;
    public bool playedOnce;

    public static DataPersistence Singleton
    {
        get
        {
            if (singleton == null)
            {
                singleton = new DataPersistence();
            }
            return singleton;
        }
    }

    private void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
        }
        else if (singleton != null)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        Load();
    }

    [System.Serializable]
    class SaveData
    {
        public int bestScore;
        public bool playedOnce;
    }

    public void Save()
    {
        SaveData data = new SaveData();
        data.bestScore = singleton.bestScore;
        data.playedOnce = singleton.playedOnce;
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "savefile.json", json);
    }

    public void Load()
    {
        string path = Application.persistentDataPath + "savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            singleton.bestScore = data.bestScore;
            singleton.playedOnce = data.playedOnce;
        }
    }

    public void EraseAllSaveData()
    {
        string path = Application.persistentDataPath + "savefile.json";

        if (File.Exists(path))
        {
            File.Delete(path);
        }
        GameSystem.Singleton.Quit();
    }
}
