using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    public float minSpawnTime;
    [SerializeField]
    public float maxSpawnTime;
    [SerializeField]
    public GameObject obstacle;

    public static ObstacleSpawner instance;

    public bool isSpawner = false;
    public bool startSpawning = false;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        obstacle = GameObject.Find("HazardObInit");
        //StartCoroutine("Spawn");
    }

    void Update()
    {
        if(startSpawning)
        {
            startSpawning = false;
            StartCoroutine("Spawn");
        }
    }

    IEnumerator Spawn()
    {
        float waitTime = 1.0f;
        yield return new WaitForSeconds(waitTime);
        while (isSpawner)
        {
            //print("Colliderring");
            SpawnObstacle();
            waitTime = Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(waitTime);
        }
    }

    void SpawnObstacle()
    {
        Instantiate(obstacle, transform.position, transform.rotation);
    }
}
