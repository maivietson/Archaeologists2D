using System;
using System.Collections;
using System.Collections.Generic;
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

    // sound
    [SerializeField] Image soundOff;

    // tooltip
    [SerializeField] GameObject panelTooltip;
    [SerializeField] Text contentTooltip;

    public static GameSession instance;
    public static bool IsLoadData { get; set; }

    public bool isSoundOff = false;

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

    public void AddLife(int live)
    {
        playerLives += live;
        livesText.text = playerLives.ToString();
    }

    private void ResetGameSession()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    private void TakeLife()
    {
        playerLives--;
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        livesText.text = playerLives.ToString();
    }

    public void ToolTip()
    {

    }

    public void Sound()
    {
        print("press");
        isSoundOff = !isSoundOff;
        soundOff.enabled = isSoundOff;
    }
}
