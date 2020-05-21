using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void LoadFirstLevel()
    {
        CrossParameter.FileDataLoaded = "level1.save";
        GameSession.IsLoadData = true;
        SceneManager.LoadScene(1);
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

    public void ContinueGame()
    {
        print("Continue Game");
        CrossParameter.FileDataLoaded = MenuManager.instance.GetFileSavedName();
        GameSession.IsLoadData = true;
        SceneManager.LoadScene(MenuManager.instance.GetSceneIndexSaved());
    }
}
