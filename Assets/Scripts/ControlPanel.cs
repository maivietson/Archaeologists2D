using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControlPanel : MonoBehaviour
{
    // sound
    [SerializeField] Image soundOff;

    // tooltip
    [SerializeField] GameObject panelTooltip;
    [SerializeField] Text contentTooltip;

    // settings
    [SerializeField] GameObject panelSettings;

    public bool isSoundOff = false;
    public bool isTooltip = false;
    public bool isSettings = false;

    private int currentSceneIndex;

    public static ControlPanel instance = null;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void ToolTip(string toolTipString)
    {
        isTooltip = !isTooltip;
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if(toolTipString.Length > 0)
        {
            contentTooltip.text = toolTipString;
        }
        else
        {
            contentTooltip.text = Texts.instance.GetText(Types.TextsID.ID_TOOLTIP, currentSceneIndex);
        }
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
        LoadSaveData.instance.SaveDataGame();
        isSettings = false;
        panelSettings.SetActive(isSettings);
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
