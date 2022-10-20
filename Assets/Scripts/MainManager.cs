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

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;


    public Text ScoreText;
    public Text PlayerName;
    public Text BestScore;
    public GameObject GameOverText;

    private bool m_Started = false;
    private int m_Points;

    static public bool m_GameOver = false;

    static public int bestScore;
    static public string bestPlayer;

    private void Awake()
    {
        Highscores.Instance.LoadWinnerData();
        bestPlayer = Highscores.Instance.bestPlayer;
        bestScore = Highscores.Instance.bestScore;
    }


    // Start is called before the first frame update
    void Start()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);

        int[] pointCountArray = new[] { 1, 1, 2, 2, 5, 5 };
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
        PlayerName.text = "Player Name: " + Highscores.Instance.playerName;
        SetBestPlayer();
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
        Highscores.Instance.score = m_Points;
    }

    public void GameOver()
    {
        m_GameOver = true;
        CheckBestPlayer();
        GameOverText.SetActive(true);
    }

    public void StartMenu()
    {
        SceneManager.LoadScene("StartScreen");
    }

    public void CheckBestPlayer()
    {
        if (Highscores.Instance.score >= Highscores.Instance.bestScore)
        {
            Highscores.Instance.bestPlayer = Highscores.Instance.playerName;
            Highscores.Instance.bestScore = Highscores.Instance.score;
        }
        Highscores.Instance.SaveWinnerData(Highscores.Instance.bestPlayer, Highscores.Instance.bestScore);
        BestScore.text = "Best Score - " + Highscores.Instance.bestPlayer + ": " + Highscores.Instance.bestScore;
    }

    public void SetBestPlayer()
    {
        if (Highscores.Instance.bestPlayer == null && Highscores.Instance.bestScore == 0)
        {
            BestScore.text = "  ";
        }
        else
        {
            BestScore.text = "Best Score: " + Highscores.Instance.bestPlayer + ": " + Highscores.Instance.bestScore;
        }
    }
}