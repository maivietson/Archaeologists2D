using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3;
    [SerializeField] int score = 0;
    //[SerializeField] bool isLoad = true;

    [SerializeField] Text livesText;
    [SerializeField] Text scoreText;

    public static GameSession instance;
    public static bool IsLoadData { get; set; }

    private void Awake()
    {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        if(numGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }

        if(instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        IsLoadData = true;
        livesText.text = playerLives.ToString();
        scoreText.text = score.ToString();
    }

    void Update()
    {
        //SaveGame();
    }

    public void AddToScore(int pointsToAdd)
    {
        score += pointsToAdd;
        scoreText.text = score.ToString();
    }

    public void TakeCoin(int coin)
    {
        for(int i = 0; i <= coin; i+=10)
        {
            score += i;
            scoreText.text = score.ToString();
        }
    }

    public void ProcessPlayerDeath()
    {
        if(playerLives > 1)
        {
            IsLoadData = false;
            TakeLife();
        }
        else
        {
            IsLoadData = true;
            ResetGameSession();
        }
    }

    public int GetScore()
    {
        return score;
    }

    public int GetLive()
    {
        return playerLives;
    }

    public void SetScore(int coin)
    {
        score = coin;
        scoreText.text = coin.ToString();
    }

    public void SetLive(int live)
    {
        playerLives = live;
        livesText.text = live.ToString();
    }

    public void AddLife(int live)
    {
        playerLives += live;
        livesText.text = playerLives.ToString();
    }

    private void ResetGameSession()
    {
        CleanSave();
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    private void CleanSave()
    {
        string filePath = Application.persistentDataPath + "/data/";
        foreach (string file in Directory.GetFiles(filePath))
        {
            FileInfo fileInfo = new FileInfo(file);
            string fileName = fileInfo.Name;
            if (fileName.Contains("_saved"))
            {
                File.Delete(file);
            }
        }
    }

    private void TakeLife()
    {
        playerLives--;
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        livesText.text = playerLives.ToString();
    }
}
