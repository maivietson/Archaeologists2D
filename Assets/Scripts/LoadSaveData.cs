using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSaveData : MonoBehaviour
{
    int currentSceneIndex;
    GameObject target;

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
    }

    // Start is called before the first frame update
    void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        target = GameObject.Find("Pickups");
        //SaveGame();
        LoadGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private LevelData CreateSaveGameObject()
    {
        //List<Transform> placesObjects = transform.GetComponentsInChildren<Transform>().ToList();
        List<Transform> placesObjects = target.transform.GetComponentsInChildren<Transform>().ToList();
        placesObjects.Remove(transform);

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
        string fileName = "level" + currentSceneIndex + ".save";
        FileStream file = File.Create(Application.persistentDataPath + "/" + fileName);
        bf.Serialize(file, save);
        file.Close();
    }

    public void LoadGame()
    {
        // handle file
        string fileName = "level" + currentSceneIndex + ".save";
        if (File.Exists(Application.persistentDataPath + "/" + fileName))
        {
            //read file
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/" + fileName, FileMode.Open);
            LevelData save = (LevelData)bf.Deserialize(file);
            file.Close();

            //handle data
            foreach(CropData data in save.cropData)
            {
                print("Name: " + data.itemScriptableObject);
                print("Position: " + data.transformData.x + ", " + data.transformData.y);
                //GameObject instanceCoin = Instantiate(Resources.Load("Prefabs/Coin"), new Vector3(data.transformData.x, data.transformData.y, 0f), Quaternion.identity) as GameObject;
                //instanceCoin.transform.SetParent(target.transform);
            }
        }
        else
        {
            Debug.Log("No game saved!");
        }
    }
}
