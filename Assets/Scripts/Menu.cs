using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void LoadFirstLevel()
    {
        CrossParameter.FileDataLoaded = "level1.save";
        SceneManager.LoadScene(1);
    }
}
