using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float MoveSpeed;
    public float LimitX;

    private JumpInfomation _jumpInfomation = new JumpInfomation();
    private float _posY;        // 플레이어 포지션
    private float _posX;

    public JoyStickButton leftMoveBtn;
    public JoyStickButton RightMoveBtn;

    public JoyStickButton JumpBtn;
    public Transform overlapBoxTransform;

    private AbstPortal _currentPortal = null;

    private Animator _animator;

    void Start()
    {
        leftMoveBtn.SetMoveEvent(LeftMove);
        RightMoveBtn.SetMoveEvent(RightMove);

        leftMoveBtn.SetUpEvent(UpMoveBtn);
        RightMoveBtn.SetUpEvent(UpMoveBtn);
        JumpBtn.SetMoveEvent(OnJump);

        _posX = transform.position.x;
        _animator = GetComponent<Animator>();
    }

    void UpMoveBtn()
    {
        _animator.SetBool("move", false);
    }

#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
            LeftMove();
        else if (Input.GetKey(KeyCode.RightArrow))
            RightMove();

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

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (_currentPortal != null)
                _currentPortal.UsePortal(this);
        }
    }

    private void FixedUpdate()
    {
        jumpProcess();

        CheckHorizontalRaycast();

        _posX = Mathf.Clamp(_posX, -LimitX, LimitX);

        gameObject.transform.position = new Vector3(_posX, _posY);
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
                _jumpInfomation.BaseY = -4.32f;
                _jumpInfomation.JumpState = 2;
            }
        }
    }

    public void LeftMove()
    {
        _posX += -1f * MoveSpeed * Time.deltaTime;
        transform.localScale = new Vector3(-1f, 1f, 1f);
        _animator.SetBool("move", true);
    }

    public void RightMove()
    {
        _posX += 1f * MoveSpeed * Time.deltaTime;
        transform.localScale = new Vector3(1f, 1f, 1f);
        _animator.SetBool("move", true);
    }

    public void OnJump()
    {
        if (_jumpInfomation.JumpState == 0)
        {
            _jumpInfomation.JumpState = 1;
            _jumpInfomation.Gravity = _jumpInfomation.Jump_speed;
            _animator.SetBool("jump", true);
        }
    }

    public void EntryPortal(AbstPortal newPortal)
    {
        _currentPortal = newPortal;
    }

    public void ExitPortal()
    {
        _currentPortal = null;
    }
}
