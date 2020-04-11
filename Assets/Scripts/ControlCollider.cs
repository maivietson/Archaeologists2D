using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlCollider : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Vector2 bound = gameObject.GetComponent<SpriteRenderer>().sprite.bounds.size;
        //print("size sprite: " + bound);
        gameObject.GetComponent<BoxCollider2D>().size = bound;
        gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(0, 0);
    }
}
