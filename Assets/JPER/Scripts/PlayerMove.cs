using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float MoveSpeed;

    private JumpInfomation _jumpInfomation = new JumpInfomation();
    private float _posY;        // 플레이어 포지션

    public JoyStickButton leftMoveBtn;
    public JoyStickButton RightMoveBtn;

    public JoyStickButton JumpBtn;
    public Transform overlapBoxTransform;

    private AbstPortal _currentPortal = null;

    void Start()
    {
        leftMoveBtn.SetMoveEvent(LeftMove);
        RightMoveBtn.SetMoveEvent(RightMove);
        JumpBtn.SetMoveEvent(OnJump);
    }

#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
            LeftMove();
        else if (Input.GetKey(KeyCode.RightArrow))
            RightMove();

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
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, _posY);
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
        transform.localPosition += Vector3.left * MoveSpeed * Time.deltaTime; ;
        transform.localScale = new Vector3(-1f, 1f, 1f);
    }

    public void RightMove()
    {
        transform.localPosition += Vector3.right * MoveSpeed * Time.deltaTime;
        transform.localScale = new Vector3(1f, 1f, 1f);
    }

    public void OnJump()
    {
        if (_jumpInfomation.JumpState == 0)
        {
            _jumpInfomation.JumpState = 1;
            _jumpInfomation.Gravity = _jumpInfomation.Jump_speed;
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
