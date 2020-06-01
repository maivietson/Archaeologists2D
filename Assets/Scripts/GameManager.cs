using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class GameManager : MonoBehaviour
{
    [SerializeField] private AssetReference _addressableTextAsset = null;

    private string jsonData;
    private Data listData;

    // Start is called before the first frame update
    void Start()
    {
        _addressableTextAsset.LoadAssetAsync<TextAsset>().Completed += handle =>
        {
            listData = CreateDataFromJson(handle.Result.text);
            PrepareDataInitialization(listData);
            Addressables.Release(handle);
        };
    }

    private Data CreateDataFromJson(string jsonString)
    {
        return JsonUtility.FromJson<Data>(jsonString);
    }

    private void PrepareDataInitialization(Data data)
    {
        foreach(Level lv in data.listLevel)
        {
            string path = Application.persistentDataPath + "/data/";
            if(!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string filePath = path + lv.nameLevel + ".save";

            if (!System.IO.File.Exists(filePath))
            {
                // Create initialization data
                LevelData save = CreateDataInitialization(lv);

                // make file initialization data
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Create(filePath);
                bf.Serialize(file, save);
                file.Close();
            }
        }
    }

    private LevelData CreateDataInitialization(Level level)
    {
        LevelData data = new LevelData();

        foreach(Position pos in level.listPosition)
        {
            CropData obData = new CropData();
            obData.transformData = new TransformData(pos.posX, pos.posY, pos.posZ);
            obData.itemScriptableObject = level.objectName;
            data.cropData.Add(obData);
        }

        return data;
    }
}
