using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSaveData : MonoBehaviour
{
    int currentSceneIndex;
    bool firstLoad = true;

    GameObject target;

    private static LoadSaveData instance = null;

    public static LoadSaveData Instance
    {
        get
        {
            return instance;
        }
    }

    private void wake()
    {
        int numLoadSaveData = FindObjectsOfType<LoadSaveData>().Length;
        if(numLoadSaveData > 1)
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
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        target = GameObject.Find("Pickups");
        //SaveGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private LevelData CreateSaveGameObject()
    {
        //List<Transform> placesObjects = transform.GetComponentsInChildren<Transform>().ToList();
        List<Transform> placesObjects = target.transform.GetComponentsInChildren<Transform>().ToList();
        placesObjects.RemoveAt(0);

        LevelData data = new LevelData();

        foreach(Transform t in placesObjects)
        {
            CropData cropData = new CropData();
            cropData.transformData = new TransformData(t.localPosition.x, t.localPosition.y);
            cropData.itemScriptableObject = t.name;
            print("Name of object: " + t.name);
            data.cropData.Add(cropData);
        }

        return data;
    }

    public void SaveGame()
    {
        // create save
        LevelData save = CreateSaveGameObject();

        // binary data
        BinaryFormatter bf = new BinaryFormatter();
        string fileName = "level" + currentSceneIndex + "_played.save";
        FileStream file = File.Create(Application.persistentDataPath + "/" + fileName);
        bf.Serialize(file, save);
        file.Close();
    }

    public void LoadScene(bool firstTime)
    {
        if(firstTime)
        {
            string fileName = "level" + currentSceneIndex + ".save";
            LoadDataGame(fileName);
        }
        else
        {
            string fileName = "level" + currentSceneIndex + "_played.save";
            LoadDataGame(fileName);
        }
    }

    public void LoadDataGame(string fileName)
    {
        // handle file
        if (File.Exists(Application.persistentDataPath + "/" + fileName))
        {
            //read file
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/" + fileName, FileMode.Open);
            LevelData save = (LevelData)bf.Deserialize(file);
            file.Close();
            
            //handle data
            foreach (CropData data in save.cropData)
            {
                if(data.itemScriptableObject.Contains("Pickups"))
                {
                    continue;
                }
                print("Name: " + data.itemScriptableObject);
                print("Position: " + data.transformData.x + ", " + data.transformData.y);
                GameObject coin = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Coin.prefab", typeof(GameObject)) as GameObject;
                GameObject instanceCoin = Instantiate(coin, new Vector3(data.transformData.x, data.transformData.y, 0f), Quaternion.identity) as GameObject;
                instanceCoin.name = "Coin";
                instanceCoin.transform.SetParent(target.transform);
            }
        }
        else
        {
            Debug.Log("No game saved!");
        }
    }

    public void SetStateData(bool value)
    {
        firstLoad = value;
    }
}
