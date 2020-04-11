using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardObstacle : MonoBehaviour
{
    PolygonCollider2D objectCollider;

    // Start is called before the first frame update
    void Start()
    {
        objectCollider = GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        ColliderWithGround();
    }

    private void ColliderWithGround()
    {
        if(objectCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            Destroy(gameObject);
        }
    }
}
