using System.Collections.Generic;
using System;

[Serializable]
public class Data
{
    public List<Level> listLevel = new List<Level>();
}

[Serializable]
public class Level
{
    public string nameLevel;
    public string objectName;
    public List<Position> listPosition = new List<Position>();
}

[Serializable]
public class Position
{
    public float posX;
    public float posY;
    public float posZ;
}
