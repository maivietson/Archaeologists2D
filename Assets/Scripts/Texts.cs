using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using LitJson;

public class Texts : MonoBehaviour
{
    // handle string json
    private string jsonString;
    private JsonData data;

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
        jsonString = File.ReadAllText(Application.dataPath + "/DataGame/TextInGame.json");
        data = JsonMapper.ToObject(jsonString);
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
