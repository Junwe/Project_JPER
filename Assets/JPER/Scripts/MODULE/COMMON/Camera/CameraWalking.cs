using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 카메라가 Target을 따라가게 만드는 기능.
public class CameraWalking : MonoBehaviour
{
    public Transform Target;

    private float _moveSpeed = 4f;

    private float LimitX = 30.49f;

    private bool _isTarget = true;
    private Camera _thisCamera;
    // Start is called before the first frame update
    void Start()
    {
        _thisCamera = GetComponent<Camera>();
        //StartCoroutine(Walking());
        // if (StageManager.SelectStage != null)
        //     LimitX = StageManager.SelectStage.LimitX;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_isTarget == false) return;
        Vector3 targetPos = Target.transform.localPosition;
        targetPos = new Vector3(targetPos.x, targetPos.y, -10f);
        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPos, Time.deltaTime * _moveSpeed);
    }

    IEnumerator Walking()
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();
        }
    }

    public void SetCameraSize(float size, float time)
    {
        StartCoroutine(Tween.Instance.CameraSize(_thisCamera, _thisCamera.orthographicSize, size, time, 0f, AnimationCurve.EaseInOut(0f, 0f, 1f, 1f)));
    }
}
