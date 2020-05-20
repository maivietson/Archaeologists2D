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

    // sound
    [SerializeField] Image soundOff;

    // tooltip
    [SerializeField] GameObject panelTooltip;
    [SerializeField] Text contentTooltip;

    // settings
    [SerializeField] GameObject panelSettings;

    public static GameSession instance;
    public static bool IsLoadData { get; set; }

    public bool isSoundOff = false;
    public bool isTooltip = false;
    public bool isSettings = false;

    private int currentSceneIndex;

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
        isTooltip = !isTooltip;
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        contentTooltip.text = Texts.instance.GetText(Types.TextsID.ID_TOOLTIP, currentSceneIndex);
        panelTooltip.SetActive(isTooltip);
        StartCoroutine("DelayTime");
    }

    IEnumerator DelayTime()
    {
        yield return new WaitForSeconds(5);
        panelTooltip.SetActive(false);
        isTooltip = false;
    }

    public void Sound()
    {
        print("press");
        isSoundOff = !isSoundOff;
        soundOff.enabled = isSoundOff;
    }

    public void Settings()
    {
        isSettings = !isSettings;
        panelSettings.SetActive(isSettings);
    }

    public void SaveGame()
    {
        print("Save Game");
        foreach (string file in Directory.GetFiles("Assets/DataGame/"))
        {
            FileInfo fileInfo = new FileInfo(file);
            string fileName = fileInfo.Name;
            if(fileName.Contains("_saved"))
            {
                File.Delete(file);
            } 
        }
        LoadSaveData.instance.SaveDataGame();
    }

    public void QuitGame()
    {
        print("Quit Game");
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                 Application.Quit();
        #endif
    }
}
