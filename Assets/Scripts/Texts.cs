using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using LitJson;
using UnityEngine.AddressableAssets;

public class Texts : MonoBehaviour
{
    // handle string json
    private string jsonString;
    private JsonData data;

    // load text from addressable
    [SerializeField] private AssetReference _addressableTextAsset = null;

    // instance 
    public static Texts instance;

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
        _addressableTextAsset.LoadAssetAsync<TextAsset>().Completed += handle =>
        {
            CreateDataFromJson(handle.Result.text);
            print(handle.Result.text);
            Addressables.Release(handle);
        };
    }

    private void CreateDataFromJson(string textContent)
    {
        data = JsonMapper.ToObject(textContent);
    }

    public JsonData GetData()
    {
        return data;
    }

    public string GetText(string parent, string id)
    {
        return data[parent][id].ToString();
    }

    public string GetText(ushort parent, ushort id)
    {
        return data[parent][id].ToString();
    }

    public string GetText(Types.TextsID textID, int id)
    {
        return data[(int)textID][id].ToString();
    }

    public string GetText(Types.TextsID textID, ushort id)
    {
        return data[(int)textID][id].ToString();
    }
}
