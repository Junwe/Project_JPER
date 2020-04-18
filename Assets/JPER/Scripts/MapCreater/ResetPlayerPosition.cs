using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MapObject))]
public class ResetPlayerPosition : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var rewind = collision.gameObject.GetComponent<PlayerRewind>();
        if (rewind != null)
            rewind.StartRewind(goToStartPoint: true);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawIcon(transform.position, "Prohibition.png");
    }
}
