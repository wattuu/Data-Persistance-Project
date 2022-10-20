using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuManager : MonoBehaviour
{
    public string playerName;
    public TMP_InputField nameInput;
    public Text HighScore;
    //public Highscores highScores;


    // Start is called before the first frame update
    void Start()
    {
        Highscores.Instance.LoadWinnerData();
        HighScore.text = "Best Score: " + Highscores.Instance.bestPlayer + ": " + Highscores.Instance.bestScore;
    }

    public void SaveName()
    {
        playerName = nameInput.text;
        Highscores.Instance.playerName = playerName;
    }
    
    public void StartMain()
    {
        SaveName();
        SceneManager.LoadScene("main");
    }


    public void Exit()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

}
