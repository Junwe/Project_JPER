using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float MoveSpeed;
    public float JumpHeight;

    private float _gravity;     // 중력 점프 할때 중력을 높여줌
    private int _jumpState = 0; // 점프 상태(0 : 정지 , 1 : 점프중, 2 : 떨어지는중) 
    private float _posY;        // 플레이어 포지션
    private float _baseY = -4.32f;  // y 최소 좌표
    const float _jump_speed = 0.2f;  // 점프속도
    const float _jump_accell = 0.01f; // 점프가속
    const float _y_base = 0.5f;      // 캐릭터가 서있는 기준점

    public JoyStickButton leftMoveBtn;
    public JoyStickButton RightMoveBtn;

    private BoxCollider2D boxCollider = null;

    void Start()
    {
        leftMoveBtn.SetMoveEvent(LeftMove);
        RightMoveBtn.SetMoveEvent(RightMove);

        boxCollider = GetComponent<BoxCollider2D>();
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

    }

    private void FixedUpdate()
    {
        jumpProcess();

        CheckHorizontalRaycast();
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, _posY);
    }
#endif

    private void jumpProcess()
    {
        switch (_jumpState)
        {
            case 0: // 가만히 있는 상태
                {
                    if (_posY > _baseY) // basey위치로 좌표시키게 한다.
                    {
                        if (_posY >= _jump_accell)
                        {
                            _posY -= _gravity;
                        }
                        else
                        {
                            _posY = _baseY;
                        }
                    }
                    break;
                }
            case 1: // up
                {
                    _posY += _gravity;
                    if (_gravity <= 0.0f)
                    {
                        _jumpState = 2;
                    }
                    else
                    {
                        _gravity -= _jump_accell;   // 점프값이 점점 줄어들게
                    }
                    break;
                }

            case 2: // down
                {
                    _posY -= _gravity;
                    if (_posY > _baseY)
                    {
                        _gravity += _jump_accell;   // 점프값이 점점 늘어나게
                    }
                    else
                    {
                        _jumpState = 0;
                        _posY = _baseY;
                    }
                    break;
                }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.matrix = Matrix4x4.TRS(transform.position + new Vector3(0.4f * transform.localScale.x, 0f, 0f),
                                      Quaternion.identity, 
                                      new Vector3(1.5f, 0.2f, 1f));
        Gizmos.color = Color.red;
        Gizmos.DrawCube(Vector3.zero, Vector3.one);
    }

    private void CheckHorizontalRaycast()
    {
        int layermask = 1 << LayerMask.NameToLayer("wall");
        Collider2D col = Physics2D.OverlapBox(transform.position + new Vector3(0.4f * transform.localScale.x, 0f, 0f),
                                              new Vector3(1.5f, 0.2f, 1f), 
                                              0.0f, 
                                              layermask);

        if (col != null)
        {
            if (_jumpState == 2)
            {
                _baseY = col.transform.position.y + 0.4f;
                _posY = col.transform.position.y + 0.4f;
                _jumpState = 0;
            }
        }
        else
        {
            if (_jumpState == 0)
            {
                Debug.Log("_jumpstate");
                _baseY = -4.32f;
                _jumpState = 2;
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
        Debug.Log(_jumpState);
        if (_jumpState == 0)
        {
            _jumpState = 1;
            _gravity = _jump_speed;
        }
    }
}
