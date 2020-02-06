using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 카메라가 Target을 따라가게 만드는 기능.
public class CameraWalking : MonoBehaviour
{
    public Transform Target;

    private float _moveSpeed = 4f;

    private float LimitX = 30.49f;
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(Walking());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 targetPos = Target.transform.localPosition;
        targetPos = new Vector3(targetPos.x, targetPos.y, -10f);
        //transform.localPosition = new Vector3(Target.localPosition.x, Target.localPosition.y, -10f);
        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPos, Time.deltaTime * _moveSpeed);
        transform.localPosition = new Vector3(Mathf.Clamp(transform.localPosition.x, -LimitX, LimitX), transform.localPosition.y, -10f);
        
    }

    IEnumerator Walking()
    {
        while(true)
        {
            yield return new WaitForEndOfFrame();
        }
    }
}
