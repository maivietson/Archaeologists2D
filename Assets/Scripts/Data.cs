using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LevelData
{
    public List<CropData> cropData = new List<CropData>();
}

[Serializable]
public class CropData
{
    public TransformData transformData = new TransformData(0, 0);
    public string itemScriptableObject = "";
}

[Serializable]
public class TransformData
{
    public TransformData(float _x, float _y)
    {
        x = _x;
        y = _y;
    }

    public float x;
    public float y;
}
