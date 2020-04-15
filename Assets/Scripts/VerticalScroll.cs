using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalScroll : MonoBehaviour
{
    [Tooltip("Game units per second")]
    [SerializeField] float scrollRate = 0.2f;
    [SerializeField] float limitMaxPosY;
    [SerializeField] float linitMinPosY;

    float moveY;

    void Update()
    {
        float positionY = gameObject.transform.localPosition.y;
        if(positionY >= limitMaxPosY)
        {
            moveY = -scrollRate * Time.deltaTime * 2;
        }

        if(positionY <= linitMinPosY)
        {
            moveY = scrollRate * Time.deltaTime * 2;
        }
        transform.Translate(new Vector2(0, moveY));
    }
}
