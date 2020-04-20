using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class PlayerRewind : MonoBehaviour
{
    public Material MatDissovle;
    public Material MatHDR;
    [SerializeField]
    private float immotalModeTime = 2.5f;

    private PlayerMove _trTarget;
    private Vector3 _lastJumpStartPosition;
    private int _dropCount = 0;

    private PlayerAnimation _animation;

    private GameObject _startPointObject = null;

    private BoxCollider2D _playerCollider = null;
    private WaitForSeconds _waitForSecondsCache = null;
    private SpriteRenderer _sprPlayer;

    void Awake()
    {
        _animation = GetComponent<PlayerAnimation>();

        _trTarget = GetComponentInChildren<PlayerMove>();
        _sprPlayer = GetComponentInChildren<SpriteRenderer>();

        _playerCollider = GameObject.FindWithTag("Player").GetComponent<BoxCollider2D>();
        _waitForSecondsCache = new WaitForSeconds(immotalModeTime);
    }

    void Start()
    {
        _startPointObject = GameObject.FindWithTag("StartPoint");
        if (_startPointObject != null)
            _trTarget.SetPlayerPosition(_startPointObject.transform.position.x, _startPointObject.transform.position.y);
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
        if (_animation.IsDissovling)
            return;

        _animation.Animator.SetTrigger("Dissovle");
        _animation.IsDissovling = true;
        _animation.isResetToStartPoint = goToStartPoint;
    }

    public void StartReSetPosition()
    {
        if (_animation.isResetToStartPoint == false)
        {
            _trTarget.SetPlayerLocalPosition(_lastJumpStartPosition.x, _lastJumpStartPosition.y + 0.2f);
            _playerCollider.enabled = false;
        }
        else if (_startPointObject != null && _animation.isResetToStartPoint == true)
            _trTarget.SetPlayerLocalPosition(_startPointObject.transform.position.x, _startPointObject.transform.position.y);

        _animation.Animator.SetTrigger("OffDissovle");
        _animation.isResetToStartPoint = false;
    }

    public void SetImmortalMode()
    {
        StopCoroutine(ImmortalMode());
        StartCoroutine(ImmortalMode());
    }

    private IEnumerator ImmortalMode()
    {
        if (_playerCollider == null)
            yield break;

        _playerCollider.enabled = false;
        _sprPlayer.material = MatHDR;

        Coroutine crtColor = StartCoroutine(Tween.Instance.SetColorPingPong(_sprPlayer, new Color(1f, 1f, 1f, 1.0f), new Color(1f, 1f, 1f, 0.5f), immotalModeTime / 14f, 14));

        yield return _waitForSecondsCache;
        StopCoroutine(crtColor);
        _sprPlayer.SetAlpha(1f);
        _sprPlayer.material = MatDissovle;
        _playerCollider.enabled = true;
    }
}
