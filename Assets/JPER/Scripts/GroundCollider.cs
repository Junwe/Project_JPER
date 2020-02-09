using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class GroundCollider : MonoBehaviour
{
    private BoxCollider2D boxCollider = null;

    public float GroundHeight
    {
        get
        {
            Vector3 localHeight = 
                transform.localPosition + 
                new Vector3(boxCollider.offset.x, boxCollider.offset.y, 0) +
                new Vector3(0, boxCollider.size.y * 0.5f, 0);

            return localHeight.y;
        }
    }

    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }
}
