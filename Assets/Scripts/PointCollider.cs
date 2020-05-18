using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointCollider : MonoBehaviour
{
    private bool isStart = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && !isStart)
        {
            print("Collider");
            isStart = true;
            WaterMove.instance.isPause = false;
        }
    }
}
