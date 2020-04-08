using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class PlayerMove : MonoBehaviour
{
    private struct PlayerPosition
    {
        public float PosX;
        public float PosY;
    }

    public UnityEvent JumpEvent;
    public UnityEvent FallEvent;    // 땅으로 떨어졌을 때 호출.

    public float MoveSpeed;     // 이동 속도
    public float LimitY;        // 플레이어 맵 범위

    private JumpInfomation _jumpInfomation = new JumpInfomation();
    private PlayerPosition _playerPosition = new PlayerPosition();

    public JoyStickButton leftMoveBtn;
    public JoyStickButton RightMoveBtn;

    private JoyStickButton _currentPushButton;

    public JoyStickButton ActionBtn;
    public Transform overlapBoxTransform;

    public GraphicRaycaster graphic;

    private PortalExecuter _portalExecuter = null;

    // 넉백 데이터
    private PlayerAnimation _animation;

    public KnockbackInfomation KnockbackInfomation { get; private set; } = null;

    void Awake()
    {
        leftMoveBtn.SetMoveEvent(LeftMove);
        RightMoveBtn.SetMoveEvent(RightMove);

        leftMoveBtn.SetUpEvent(UpMoveBtn);
        RightMoveBtn.SetUpEvent(UpMoveBtn);
        ActionBtn.SetMoveEvent(OnPlayerAction);
        _portalExecuter = GetComponent<PortalExecuter>();

        _currentPushButton = RightMoveBtn;

        KnockbackInfomation = new KnockbackInfomation(this, _jumpInfomation);
    }

    void UpMoveBtn()
    {
        _animation.Animator.SetBool("move", false);
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (KnockbackInfomation.KnuckbackFlag == false)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
                LeftMove();
            else if (Input.GetKey(KeyCode.RightArrow))
                RightMove();
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            UpMoveBtn();
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            UpMoveBtn();
        }

        if (Input.GetKeyDown(KeyCode.Space))
            OnPlayerAction();
#else
        MoveButtonRayCast();

        if (Input.touchCount == 0)
        {
            if (_currentPushButton != null)
                _currentPushButton.OnUp();
        }
#endif
        CheckPlayerFootOverlapedToGround();
    }

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        Gizmos.DrawCube(overlapBoxTransform.position, overlapBoxTransform.localScale);
    }
#endif
    private void MoveButtonRayCast()
    {
        var ped = new PointerEventData(null);

        ped.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();

        graphic.Raycast(ped, results);

        if (results.Count <= 0)
        {
            _currentPushButton.OnUp();
            return;
        }

        if (SetMoveButton(results[0].gameObject, leftMoveBtn))
            return;

        if (SetMoveButton(results[0].gameObject, RightMoveBtn))
            return;
    }

    private bool SetMoveButton(GameObject pushObject, JoyStickButton button)
    {
        if (pushObject.gameObject.Equals(button.gameObject))
        {
            if (!_currentPushButton.Equals(button))
            {
                _currentPushButton.OnUp();
            }
            _currentPushButton = button;
            _currentPushButton.OnDown();
            return true;
        }
        return false;
    }

    private void FixedUpdate()
    {
        if (_animation.IsDissovling) return;

        if (KnockbackInfomation.KnuckbackFlag == true)
            KnockbackInfomation.KnockbackProcess();
        else
            _jumpInfomation.JumpProcess(ref _playerPosition.PosX, ref _playerPosition.PosY, _animation.Animator);

        _playerPosition.PosX = Mathf.Clamp(_playerPosition.PosX, -StageManager.SelectStage.LimitX, StageManager.SelectStage.LimitX);
        _jumpInfomation.Gravity = Mathf.Clamp(_jumpInfomation.Gravity, -0.35f, 0.35f);

        gameObject.transform.position = new Vector3(_playerPosition.PosX, _playerPosition.PosY);
    }

    private void CheckPlayerFootOverlapedToGround()
    {
        int layermask = 1 << LayerMask.NameToLayer("wall");
        Collider2D col = Physics2D.OverlapBox(overlapBoxTransform.position,
                                              overlapBoxTransform.localScale,
                                              0.0f,
                                              layermask);

        if (col != null)
        {
            if (_jumpInfomation.JumpState == 2 && _playerPosition.PosY >= col.transform.position.y)
            {
                _jumpInfomation.BaseY = col.transform.position.y + 0.4f;
                _playerPosition.PosY = col.transform.position.y + 0.4f;
                _jumpInfomation.JumpState = 0;
                _animation.Animator.SetBool("jump", false);
                FallEvent.Invoke();
            }
        }
        else
        {
            if (_jumpInfomation.JumpState == 0)
            {
                _jumpInfomation.BaseY = LimitY;
                _jumpInfomation.JumpState = 2;
            }
        }
    }

    public void LeftMove()
    {
        if (_animation.IsDissovling) return;

        if (transform.localScale.x <= 0f)
        {
            _playerPosition.PosX += -1f * MoveSpeed * Time.deltaTime;
        }
        transform.localScale = new Vector3(-1f, 1f, 1f);
        _animation.Animator.SetBool("move", true);

        Sound.Instance.PlayEffSound(SOUND.S_MOVE, 1f, false, false);
    }

    public void RightMove()
    {
        if (_animation.IsDissovling) return;

        if (transform.localScale.x >= 0f)
        {
            _playerPosition.PosX += 1f * MoveSpeed * Time.deltaTime;
        }
        transform.localScale = new Vector3(1f, 1f, 1f);
        _animation.Animator.SetBool("move", true);
        Sound.Instance.PlayEffSound(SOUND.S_MOVE, 1f, false, false);
    }

    public void OnPlayerAction()
    {
        if (_animation.IsDissovling) return;

        if (_portalExecuter.CurrentPortal != null)
        {
            _portalExecuter.ExecutePortal();
            return;
        }

        if (_jumpInfomation.JumpState == 0)
        {
            JumpEvent.Invoke();
            _jumpInfomation.JumpState = 1;
            _jumpInfomation.Gravity = _jumpInfomation.Jump_speed;
            _animation.Animator.SetBool("jump", true);

            Sound.Instance.PlayEffSound(SOUND.S_JUMP);
        }
    }

    public void SetPlayerAnimation(PlayerAnimation animation)
    {
        _animation = animation;
    }
    public void SetPlayerLocalPosition(float posX, float posY)
    {
        _playerPosition.PosX = posX;
        _playerPosition.PosY = posY;

        _jumpInfomation.JumpState = 2;

        transform.localPosition = new Vector3(_playerPosition.PosX, _playerPosition.PosY);
    }

    public void SetPlayerPosition(float posX, float posY)
    {
        _playerPosition.PosX = posX;
        _playerPosition.PosY = posY;

        _jumpInfomation.BaseY = posY;

        transform.position = new Vector3(_playerPosition.PosX, _playerPosition.PosY);
    }

    public void AddPlayerPosition(Vector2 posToAdd)
    {
        _playerPosition.PosX += posToAdd.x;
        _playerPosition.PosY += posToAdd.y;

        transform.localPosition = new Vector3(_playerPosition.PosX, _playerPosition.PosY);
    }
}
