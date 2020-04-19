using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class PlayerRewind : MonoBehaviour
{
    private PlayerMove _trTarget;
    private Vector3 _lastJumpStartPosition;
    private int _dropCount = 0;

    private PlayerAnimation _animation;

    private GameObject startPointObject = null;

    void Awake()
    {
        _animation = GetComponent<PlayerAnimation>();

        _trTarget = GetComponentInChildren<PlayerMove>();

    }

    void Start()
    {
        startPointObject = GameObject.FindWithTag("StartPoint");
        if (startPointObject != null)
            _trTarget.SetPlayerPosition(startPointObject.transform.position.x, startPointObject.transform.position.y);
    }


    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.A))
        {
            StartRewind();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            StartRewind(true);
        }
#endif
    }

    public void SetJumpPosition()
    {
        _lastJumpStartPosition = _trTarget.transform.localPosition;
    }

    public void CheckRewindExcute()
    {
        if (Mathf.Abs(_lastJumpStartPosition.y - _trTarget.transform.localPosition.y) >= 15f)
        {
            _dropCount++;
            if (_dropCount >= 3)
            {
                if (Advertisement.IsReady("rewardedVideo"))
                {
                    GameManager.Instance.OnAdsPopUp();
                }
                _dropCount = 0;
            }
        }
        if (Mathf.Abs(_lastJumpStartPosition.y - _trTarget.transform.localPosition.y) >= 5f)
        {
            GameManager.Instance.AddFallCount();
        }
    }

    public void StartRewind(bool goToStartPoint = false)
    {
        _animation.Animator.SetTrigger("Dissovle");
        _animation.IsDissovling = true;
        _animation.isResetToStartPoint = goToStartPoint;
    }

    public void StartReSetPosition()
    {
        if (_animation.isResetToStartPoint == false)
            _trTarget.SetPlayerLocalPosition(_lastJumpStartPosition.x, _lastJumpStartPosition.y);
        else if (startPointObject != null && _animation.isResetToStartPoint == true)
            _trTarget.SetPlayerPosition(startPointObject.transform.position.x, startPointObject.transform.position.y);

        _animation.Animator.SetTrigger("OffDissovle");
        _animation.isResetToStartPoint = false;
    }
}
