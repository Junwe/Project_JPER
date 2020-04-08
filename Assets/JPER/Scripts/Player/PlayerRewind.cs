using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRewind : MonoBehaviour
{
    private PlayerMove _trTarget;
    private Vector3 _lastJumpStartPosition;
    private int _dropCount = 0;

    PlayerAnimation _animation;

    void Awake()
    {
        _animation = GetComponent<PlayerAnimation>();

        _trTarget = GetComponentInChildren<PlayerMove>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            StartRewind();
        }
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
                GameManager.Instance.OnAdsPopUp();
                _dropCount = 0;
            }
        }
        if (Mathf.Abs(_lastJumpStartPosition.y - _trTarget.transform.localPosition.y) >= 5f)
        {
            GameManager.Instance.AddFallCount();
        }
    }

    public void StartRewind()
    {
        _animation.Animator.SetTrigger("Dissovle");
        _animation.IsDissovling = true;
    }

    public void StartReSetPosition()
    {
        _trTarget.SetPlayerLocalPosition(_lastJumpStartPosition.x, _lastJumpStartPosition.y);
        _animation.Animator.SetTrigger("OffDissovle");
    }
}
