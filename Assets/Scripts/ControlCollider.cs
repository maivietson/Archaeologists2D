using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlCollider : MonoBehaviour
{
    [SerializeField] int timeLimit = 500;
    [SerializeField] float timeDelay;
    Animator anim;

    bool startAnim = false;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!startAnim)
        {
            startAnim = true;
            StartCoroutine("TurnOnAnim");
        }

        Vector2 bound = gameObject.GetComponent<SpriteRenderer>().sprite.bounds.size;
        //print("size sprite: " + bound);
        gameObject.GetComponent<BoxCollider2D>().size = bound;
        gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(0, bound.y / 2);
    }

    IEnumerator TurnOnAnim()
    {
        startAnim = true;
        yield return new WaitForSeconds(timeDelay);
        anim.SetBool("turning", true);
    }

    public void TurnOffAnimation()
    {
        anim.SetBool("turning", false);
        startAnim = false;
    }
}
