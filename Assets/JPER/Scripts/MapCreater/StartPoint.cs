using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MapObject))]
public class StartPoint : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.DrawIcon(transform.position, "StartPoint.png");
    }
}
