using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1.0f;

    Rigidbody2D myRigiBody;
    BoxCollider2D feetCollider2D;

    // Start is called before the first frame update
    void Start()
    {
        myRigiBody = GetComponent<Rigidbody2D>();
        feetCollider2D = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(IsFacingRight())
        {
            myRigiBody.velocity = new Vector2(moveSpeed, 0f);
        }
        else
        {
            myRigiBody.velocity = new Vector2(-moveSpeed, 0f);
        }
    }

    bool IsFacingRight()
    {
        return transform.localScale.x > 0;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.IsTouchingLayers(LayerMask.GetMask("Enemy")) || !collision.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            transform.localScale = new Vector2(-(Mathf.Sign(myRigiBody.velocity.x)), 1f);
        }
    }
}
