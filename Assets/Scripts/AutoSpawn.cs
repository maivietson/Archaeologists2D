using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoSpawn : MonoBehaviour
{
    [SerializeField] float timeDelay;
    [SerializeField] bool pauseSpawn = true;
    [SerializeField] GameObject obstacle;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("AutoSpawner");
    }

    IEnumerator AutoSpawner()
    {
        float waitime = 1.0f;
        yield return new WaitForSeconds(waitime);
        while(pauseSpawn)
        {
            SpawnObstacle();
            yield return new WaitForSeconds(timeDelay);
        }
    }

    private void SpawnObstacle()
    {
        Instantiate(obstacle, transform.position, transform.rotation);
    }
}
