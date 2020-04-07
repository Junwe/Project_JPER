using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour
{
    private const string ICON_FILE_NAME = "StartPoint.png";

    // Start is called before the first frame update
    void Start()
    {
        var playerMove = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>();
        playerMove.SetPlayerPosition(transform.position.x,transform.position.y);
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.DrawIcon(transform.position, ICON_FILE_NAME);
    }
#endif
}
