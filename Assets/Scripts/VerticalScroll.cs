using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalScroll : MonoBehaviour
{
    [Tooltip("Game units per second")]
    [SerializeField] float scrollRate = 0.2f;
    [SerializeField] float scaleTime = 2;
    [SerializeField] float limitMaxPosY;
    [SerializeField] float linitMinPosY;
    [SerializeField] bool isTransformX = false;

    float moveY, moveX;

    void Update()
    {
        if(!isTransformX)
        {
            float positionY = gameObject.transform.localPosition.y;
            if (positionY >= limitMaxPosY)
            {
                moveY = -scrollRate * Time.deltaTime * scaleTime;
            }

            if (positionY <= linitMinPosY)
            {
                moveY = scrollRate * Time.deltaTime * scaleTime;
            }
            transform.Translate(new Vector2(0, moveY));
        }
        else
        {
            float positionX = gameObject.transform.localPosition.x;
            if (positionX >= limitMaxPosY)
            {
                moveX = -scrollRate * Time.deltaTime * scaleTime;
            }

            if (positionX <= linitMinPosY)
            {
                moveX = scrollRate * Time.deltaTime * scaleTime;
            }
            transform.Translate(new Vector2(moveX, 0));
        }
    }
}
