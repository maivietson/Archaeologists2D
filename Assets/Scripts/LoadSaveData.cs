using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;

public class LoadSaveData : MonoBehaviour
{
    int currentSceneIndex;

    // state
    [SerializeField] bool firstLoad = true;
    [SerializeField] bool prepareData = false;
    [SerializeField] GameObject target;

    // Load object
    private GameObject coinObject;

    public static LoadSaveData instance = null;

    private void Awake()
    {
        //int numLoadSaveData = FindObjectsOfType<LoadSaveData>().Length;
        //if(numLoadSaveData > 1)
        //{
        //    Destroy(gameObject);
        //}
        //else
        //{
        //    DontDestroyOnLoad(gameObject);
        //}

        if(instance == null)
        {
            instance = this;
        }

        coinObject = GameObject.Find("CoinInit");
    }

    // Start is called before the first frame update
    void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        target = GameObject.Find("Pickups");
        if(prepareData)
        {
            PrepareSave();
        }
        else
        {
            if (GameSession.IsLoadData)
            {
                //print("IsLoadData: " + CrossParameter.FileDataLoaded);
                LoadScene(CrossParameter.FileDataLoaded);
            }
            else
            {
                //print("NoLoadData: " + CrossParameter.FileDataLoaded);
                GameSession.IsLoadData = true;
            }
        }
    }

    private LevelData PrepareDataSave()
    {
        List<Transform> placesObjects = target.transform.GetComponentsInChildren<Transform>().ToList();
        placesObjects.RemoveAt(0);

        LevelData data = new LevelData();

        foreach (Transform t in placesObjects)
        {
            CropData cropData = new CropData();
            cropData.transformData = new TransformData(t.localPosition.x, t.localPosition.y, t.localPosition.z);
            cropData.itemScriptableObject = t.name;
            data.cropData.Add(cropData);
        }

        data.numLive = GameSession.instance.GetLive();
        data.numCoin = GameSession.instance.GetScore();

        return data;
    }

    //private LevelData CreateSaveGameObject()
    //{
    //    List<Transform> placesObjects = target.transform.GetComponentsInChildren<Transform>().ToList();
    //    placesObjects.RemoveAt(0);

    //    LevelData data = new LevelData();

    //    foreach(Transform t in placesObjects)
    //    {
    //        CropData cropData = new CropData();
    //        cropData.transformData = new TransformData(t.localPosition.x, t.localPosition.y, t.localPosition.z);
    //        cropData.itemScriptableObject = t.name;
    //        data.cropData.Add(cropData);
    //    }

    //    Transform portExit = GameObject.Find("Level Exit").transform;
    //    CropData playerInfo = new CropData();
    //    playerInfo.transformData = new TransformData(portExit.localPosition.x - 1.0f, portExit.localPosition.y, portExit.localPosition.z);
    //    playerInfo.itemScriptableObject = "Player";
    //    data.cropData.Add(playerInfo);

    //    return data;
    //}

    //public void SaveGame()
    //{
    //    // create save
    //    LevelData save = CreateSaveGameObject();

    //    // binary data
    //    BinaryFormatter bf = new BinaryFormatter();
    //    string fileName = "level" + currentSceneIndex + "_played.save";
    //    string filePath = "Assets/DataGame/" + fileName;
    //    FileStream file = File.Create(filePath);
    //    bf.Serialize(file, save);
    //    file.Close();
    //}

    public void SaveDataGame()
    {
        LevelData save = PrepareDataSave();
        // binary data
        BinaryFormatter bf = new BinaryFormatter();
        string fileName = "level" + currentSceneIndex + "_saved.save";
        string filePath = Application.persistentDataPath + "/data/" + fileName;
        FileStream file = File.Create(filePath);
        bf.Serialize(file, save);
        file.Close();
    }

    public void PrepareSave()
    {
        List<Transform> placesObjects = target.transform.GetComponentsInChildren<Transform>().ToList();
        placesObjects.RemoveAt(0);

        LevelData data = new LevelData();

        foreach (Transform t in placesObjects)
        {
            if(t.name.Contains("ListPickups"))
            {
                continue;
            }
            CropData cropData = new CropData();
            cropData.transformData = new TransformData(t.localPosition.x, t.localPosition.y, t.localPosition.z);
            cropData.itemScriptableObject = t.name;
            data.cropData.Add(cropData);
        }

        // binary data
        BinaryFormatter bf = new BinaryFormatter();
        string fileName = "level" + currentSceneIndex + ".save";
        string filePath = "Assets/DataGame/" + fileName;
        FileStream file = File.Create(filePath);
        bf.Serialize(file, data);
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

    public void LoadScene(string fileName)
    {
        LoadDataGame(fileName);
    }

    public void LoadDataGame(string fileName)
    {
        string filePath = Application.persistentDataPath + "/data/" + fileName;
        // handle file
        if (File.Exists(filePath))
        {
            //read file
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(filePath, FileMode.Open);
            LevelData save = (LevelData)bf.Deserialize(file);
            file.Close();
            
            //handle data
            foreach (CropData data in save.cropData)
            {
                if(data.itemScriptableObject.Contains("Pickups"))
                {
                    continue;
                }
                //Debug.Log("Name: " + data.itemScriptableObject);
                //Debug.Log("Position: " + data.transformData.x + ", " + data.transformData.y);
#if UNITY_EDITOR
                string prefabPath = "Assets/Prefabs/" + data.itemScriptableObject + ".prefab";
                coinObject = AssetDatabase.LoadAssetAtPath(prefabPath, typeof(GameObject)) as GameObject;
#endif
                GameObject instanceCoin = Instantiate(coinObject, new Vector3(data.transformData.x, data.transformData.y, data.transformData.z), Quaternion.identity) as GameObject;
                instanceCoin.name = data.itemScriptableObject;
                if(!data.itemScriptableObject.Contains("Player"))
                {
                    instanceCoin.transform.SetParent(target.transform);
                }
            }

            if(save.numLive > 0)
            {
                GameSession.instance.SetLive(save.numLive);
            }
            if(save.numCoin > 0)
            {
                GameSession.instance.SetScore(save.numCoin);
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
