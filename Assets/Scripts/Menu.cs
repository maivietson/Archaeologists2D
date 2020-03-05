using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void LoadFirstLevel()
    {
        //LoadSaveData.Instance.SetStateData(true);
        //LoadSaveData.Instance.IsEnableLoad(true);
        SceneManager.LoadScene(1);
    }
}
