using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LoadSaveData : MonoBehaviour
{
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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Saving()
    {
        //List<Transform> placesObjects = transform.GetComponentsInChildren<Transform>().ToList();
        List<Transform> placesObjects = GameObject.Find("Pickups").transform.GetComponentsInChildren<Transform>().ToList();
        placesObjects.Remove(transform);

        LevelData data = new LevelData();

        foreach(Transform t in placesObjects)
        {
            CropData cropData = new CropData();
            cropData.transformData = new TransformData(t.localPosition.x, t.localPosition.y);
            cropData.itemScriptableObject = t.name;

            data.cropData.Add(cropData);
        }
    }
}
