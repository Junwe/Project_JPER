using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float MoveSpeed;     // 이동 속도
    public float LimitX;        // 플레이어 맵 범위
    public float LimitY;        // 플레이어 맵 범위

    private JumpInfomation _jumpInfomation = new JumpInfomation();
    private float _posY;        // 플레이어 포지션
    private float _posX;

    public JoyStickButton leftMoveBtn;
    public JoyStickButton RightMoveBtn;

    public JoyStickButton JumpBtn;
    public Transform overlapBoxTransform;

    private PortalExecuter _portalExecuter = null;

    // 넉백 데이터
    private KnockbackInfomation knockbackInfomation = new KnockbackInfomation();
    private Animator _animator;

    public bool KnuckbackFlag { get; private set; }

    void Start()
    {
        leftMoveBtn.SetMoveEvent(LeftMove);
        RightMoveBtn.SetMoveEvent(RightMove);

        leftMoveBtn.SetUpEvent(UpMoveBtn);
        RightMoveBtn.SetUpEvent(UpMoveBtn);
        JumpBtn.SetMoveEvent(OnJump);

        _posX = transform.position.x;
        KnuckbackFlag = false;
        _animator = GetComponent<Animator>();
        _portalExecuter = GetComponent<PortalExecuter>();
    }

    void UpMoveBtn()
    {
        _animator.SetBool("move", false);
    }

#if UNITY_EDITOR
    private void Update()
    {
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
            OnJump();
    }

    void OnDrawGizmos()
    {
        Gizmos.matrix = Matrix4x4.TRS(overlapBoxTransform.position,
                                      Quaternion.identity,
                                      overlapBoxTransform.localScale);
        Gizmos.color = Color.red;
        Gizmos.DrawCube(Vector3.zero, Vector3.one);
    }
#endif

    private void FixedUpdate()
    {
        if (KnuckbackFlag == true)
            KnockbackProcess();
        else
            jumpProcess();

        CheckHorizontalRaycast();

        _posX = Mathf.Clamp(_posX, -LimitX, LimitX);

        _jumpInfomation.Gravity = Mathf.Clamp(_jumpInfomation.Gravity, -2f, 2f);

        gameObject.transform.position = new Vector3(_posX, _posY);
    }

    private void jumpProcess()
    {
        switch (_jumpInfomation.JumpState)
        {
            case 0: // 가만히 있는 상태
                {
                    if (_posY > _jumpInfomation.BaseY) // basey위치로 좌표시키게 한다.
                    {
                        if (_posY >= _jumpInfomation.Jump_accell)
                        {
                            _posY -= _jumpInfomation.Gravity;
                        }
                        else
                        {
                            _posY = _jumpInfomation.BaseY;
                        }
                    }
                    break;
                }
            case 1: // up
                {
                    _posY += _jumpInfomation.Gravity;
                    if (_jumpInfomation.Gravity <= 0.0f)
                    {
                        _jumpInfomation.JumpState = 2;
                    }
                    else
                    {
                        _jumpInfomation.Gravity -= _jumpInfomation.Jump_accell;   // 점프값이 점점 줄어들게
                    }
                    break;
                }
            case 2: // down
                {
                    _posY -= _jumpInfomation.Gravity;
                    if (_posY > _jumpInfomation.BaseY)
                    {
                        _jumpInfomation.Gravity += _jumpInfomation.Jump_accell;   // 점프값이 점점 늘어나게
                    }
                    else
                    {
                        _animator.SetBool("jump", false);
                        _jumpInfomation.JumpState = 0;
                        _posY = _jumpInfomation.BaseY;
                    }
                    break;
                }
        }
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
            if (_jumpInfomation.JumpState == 2 && transform.position.y >= col.transform.position.y)
            {
                _jumpInfomation.BaseY = col.transform.position.y + 0.4f;
                _posY = col.transform.position.y + 0.4f;
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
        _posX += -1f * MoveSpeed * Time.deltaTime;
        transform.localScale = new Vector3(-1f, 1f, 1f);
        _animator.SetBool("move", true);

        Sound.Instance.PlayEffSound(SOUND.S_MOVE, 1f, false, false);
    }

    public void RightMove()
    {
        _posX += 1f * MoveSpeed * Time.deltaTime;
        transform.localScale = new Vector3(1f, 1f, 1f);
        _animator.SetBool("move", true);
        Sound.Instance.PlayEffSound(SOUND.S_MOVE, 1f, false, false);
    }

    public void OnJump()
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
        _posX += (knockbackInfomation.horizontalAcceleration - KnockbackInfomation.FRACTION) * knockbackInfomation.direction;
        _posY += knockbackInfomation.verticalAcceleration;
        knockbackInfomation.verticalAcceleration -= KnockbackInfomation.GRAVITY; // 올라가는 속도 점점 느려지게 수직 가속도 감소.

        if (knockbackInfomation.isUp == true)
        {
            if (knockbackInfomation.verticalAcceleration <= 0.0f)
                knockbackInfomation.isUp = false;
        }
        else
        {
            if (_posY <= knockbackInfomation.groundY)
            {
                _posY = knockbackInfomation.groundY;
                KnuckbackFlag = false;
            }
        }
    }

    public void OnKnockback(int direction, float verticalAcceleration = KnockbackInfomation.DEFAULT_VERTICAL_SPEED, float horizontalAcceleration = KnockbackInfomation.DEFAULT_HORIZONTAL_SPEED)
    {
        if (KnuckbackFlag == false)
        {
            knockbackInfomation.direction = direction;
            knockbackInfomation.verticalAcceleration = verticalAcceleration;
            knockbackInfomation.horizontalAcceleration = horizontalAcceleration;
            knockbackInfomation.groundY = _posY;
            knockbackInfomation.isUp = true;
            KnuckbackFlag = true;
        }
    }
}
