using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterMove : MonoBehaviour
{
    [Tooltip("Game units per second")]
    [SerializeField] float scrollRate = 0.2f;
    [SerializeField] float scaleTime = 2;
    [SerializeField] float limitMaxPosY;
    [SerializeField] float linitMinPosY;

    public bool isPause = true;
    private float moveY, moveX;
    private bool firstRun = true;

    public static WaterMove instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPause)
        {
            float positionY = gameObject.transform.localPosition.y;
            if (positionY > limitMaxPosY)
            {
                moveY = -scrollRate * Time.deltaTime * scaleTime;
            }

            if (positionY < linitMinPosY)
            {
                moveY = scrollRate * Time.deltaTime * scaleTime;
            }

            if (firstRun && ((linitMinPosY <= positionY) && (positionY <= limitMaxPosY)))
            {
                //print("first run");
                firstRun = false;
                moveY = scrollRate * Time.deltaTime * scaleTime;
            }
            transform.Translate(new Vector2(0, moveY));
        }
    }
}
