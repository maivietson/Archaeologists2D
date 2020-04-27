using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlCollider : MonoBehaviour
{
    [SerializeField] bool isControler = false;

    int timeDelay = 0;
    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        if(isControler)
        {
            anim.enabled = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(anim.enabled == true)
        {
            Vector2 bound = gameObject.GetComponent<SpriteRenderer>().sprite.bounds.size;
            //print("size sprite: " + bound);
            gameObject.GetComponent<BoxCollider2D>().size = bound;
            gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(0, 0);
        }
    }
}
