using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class PlayerMove : MonoBehaviour
{
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
    private KnockbackInfomation knockbackInfomation = new KnockbackInfomation();
    private Animator _animator;

    public bool KnuckbackFlag { get; private set; }

    void Awake()
    {
        leftMoveBtn.SetMoveEvent(LeftMove);
        RightMoveBtn.SetMoveEvent(RightMove);

        leftMoveBtn.SetUpEvent(UpMoveBtn);
        RightMoveBtn.SetUpEvent(UpMoveBtn);
        ActionBtn.SetMoveEvent(OnPlayerAction);

        _playerPosition.PosX = transform.position.x;
        KnuckbackFlag = false;
        _animator = GetComponent<Animator>();
        _portalExecuter = GetComponent<PortalExecuter>();

        _currentPushButton = RightMoveBtn;
    }

    void UpMoveBtn()
    {
        _animator.SetBool("move", false);
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (KnuckbackFlag == false)
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
#endif
        MoveButtonRayCast();

        if (Input.touchCount == 0)
        {
            if (_currentPushButton != null)
                _currentPushButton.OnUp();
        }

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
        CheckHorizontalRaycast();

        if (KnuckbackFlag == true)
            KnockbackProcess();
        else
            _jumpInfomation.JumpProcess(_playerPosition, _animator);

        _playerPosition.PosX = Mathf.Clamp(_playerPosition.PosX, -StageManager.SelectStage.LimitX, StageManager.SelectStage.LimitX);
        _jumpInfomation.Gravity = Mathf.Clamp(_jumpInfomation.Gravity, -0.35f, 0.35f);

        gameObject.transform.position = new Vector3(_playerPosition.PosX, _playerPosition.PosY);
    }

    private void CheckHorizontalRaycast()
    {
        int layermask = 1 << LayerMask.NameToLayer("wall");
        Collider2D col = Physics2D.OverlapBox(overlapBoxTransform.position,
                                              overlapBoxTransform.localScale,
                                              0.0f,
                                              layermask);

        if (col != null)
        {
            if (_jumpInfomation.JumpState == 2 && overlapBoxTransform.transform.position.y >= col.transform.position.y + 0.2f)
            {
                _jumpInfomation.BaseY = col.transform.position.y + 0.4f;
                _playerPosition.PosY = col.transform.position.y + 0.4f;
                _jumpInfomation.JumpState = 0;
                _animator.SetBool("jump", false);
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
        _playerPosition.PosX += -1f * MoveSpeed * Time.deltaTime;
        transform.localScale = new Vector3(-1f, 1f, 1f);
        _animator.SetBool("move", true);

        Sound.Instance.PlayEffSound(SOUND.S_MOVE, 1f, false, false);
    }

    public void RightMove()
    {
        _playerPosition.PosX += 1f * MoveSpeed * Time.deltaTime;
        transform.localScale = new Vector3(1f, 1f, 1f);
        _animator.SetBool("move", true);
        Sound.Instance.PlayEffSound(SOUND.S_MOVE, 1f, false, false);
    }

    public void OnPlayerAction()
    {
        if (_portalExecuter.CurrentPortal != null)
        {
            _portalExecuter.ExecutePortal();
            return;
        }

        if (_jumpInfomation.JumpState == 0)
        {
            _jumpInfomation.JumpState = 1;
            _jumpInfomation.Gravity = _jumpInfomation.Jump_speed;
            _animator.SetBool("jump", true);

            Sound.Instance.PlayEffSound(SOUND.S_JUMP);
        }
    }

    public void KnockbackProcess()
    {
        _playerPosition.PosX += knockbackInfomation.horizontalAcceleration * knockbackInfomation.direction;
        _playerPosition.PosY += knockbackInfomation.verticalAcceleration;
        knockbackInfomation.verticalAcceleration -= KnockbackInfomation.GRAVITY; // 올라가는 속도 점점 느려지게 수직 가속도 감소.

        knockbackInfomation.verticalAcceleration = Mathf.Clamp(knockbackInfomation.verticalAcceleration, -0.35f, 10.35f);

        if (knockbackInfomation.isUp == true && knockbackInfomation.verticalAcceleration <= 0.0f)
        {
            knockbackInfomation.isUp = false;
            _jumpInfomation.JumpState = 2;
        }

        if (_jumpInfomation.JumpState == 0)
            KnuckbackFlag = false;
    }

    public void OnKnockback(int direction, float verticalAcceleration = KnockbackInfomation.DEFAULT_VERTICAL_SPEED, float horizontalAcceleration = KnockbackInfomation.DEFAULT_HORIZONTAL_SPEED)
    {
        if (KnuckbackFlag == false)
        {
            knockbackInfomation.direction = direction;
            knockbackInfomation.verticalAcceleration = verticalAcceleration;
            knockbackInfomation.horizontalAcceleration = horizontalAcceleration;
            knockbackInfomation.isUp = true;
            KnuckbackFlag = true;

            _jumpInfomation.JumpState = 1;
        }
    }

    public void UpdatePlayerPosition(Vector2 newPosition)
    {
        _playerPosition.PosX = newPosition.x;
        _playerPosition.PosY = newPosition.y;
    }
}
