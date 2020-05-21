using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] Button btnContinue;

    private bool isContinueGame = false;
    private int sceneIndex;
    private string fileSavedName;

    public static MenuManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if(CheckHasSaved())
        {
            print("Show button continue");
            btnContinue.gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private bool CheckHasSaved()
    {
        foreach (string file in Directory.GetFiles("Assets/DataGame/"))
        {
            FileInfo fileInfo = new FileInfo(file);
            string fileName = fileInfo.Name;
            if (fileName.Contains("_saved"))
            {
                fileSavedName = fileName;
                sceneIndex = int.Parse(fileName[5].ToString());
                return true;
            }
        }
        return false;
    }

    public string GetFileSavedName()
    {
        return fileSavedName;
    }

    public int GetSceneIndexSaved()
    {
        return sceneIndex;
    }
}
