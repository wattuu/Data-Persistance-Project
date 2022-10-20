using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Highscores : MonoBehaviour
{
    public static Highscores Instance;
    public string playerName;
    public int score;
    public string bestPlayer;
    public int bestScore;

    public void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    [System.Serializable]
    class SaveData
    {
        public string savePlayer;
        public int saveScore;
    }

    public void SaveWinnerData(string bestPlayer, int bestScore)
    {
        SaveData data = new SaveData();
        data.savePlayer = bestPlayer;
        data.saveScore = bestScore;
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }
    public void LoadWinnerData()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            bestPlayer = data.savePlayer;
            bestScore = data.saveScore;
        }
    }

}
