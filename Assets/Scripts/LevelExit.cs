using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField] float LevelLoadDelay = 2.0f;
    [SerializeField] float LevelExitSlowMoFactor = 0.2f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // save data before next level
        FindObjectOfType<LoadSaveData>().SaveGame();
        StartCoroutine(LoadNextLevel());
    }

    IEnumerator LoadNextLevel()
    {
        Time.timeScale = LevelExitSlowMoFactor;
        yield return new WaitForSecondsRealtime(LevelLoadDelay);
        Time.timeScale = 1f;

        // setup inilization for next scene
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        CrossParameter.FileDataLoaded = "level" + (currentSceneIndex + 1) + ".save";
        GameSession.IsLoadData = true;

        Destroy(GameObject.Find("ScenePersist"));
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}
