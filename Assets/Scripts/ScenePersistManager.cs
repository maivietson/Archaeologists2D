using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenePersistManager : MonoBehaviour
{
    private List<GameObject> listScenePersist;

    private void Awake()
    {
        int numScene = FindObjectsOfType<ScenePersistManager>().Length;
        if(numScene > 1)
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
}
