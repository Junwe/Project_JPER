using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpInfomation
{
    public float Gravity;     // 중력 점프 할때 중력을 높여줌
    public int JumpState = 0; // 점프 상태(0 : 정지 , 1 : 점프중, 2 : 떨어지는중) 
    public float BaseY = 0f;  // y 최소 좌표
    public float Jump_speed = 0.2f;  // 점프속도
    public float Jump_accell = 0.01f; // 점프가속

    public void JumpProcess(PlayerPosition _playerPosition, Animator _animator)
    {
        switch (JumpState)
        {
            case 0: // 가만히 있는 상태
                {
                    if (_playerPosition.PosY > BaseY) // basey위치로 좌표시키게 한다.
                    {
                        if (_playerPosition.PosY >= BaseY)
                        {
                            _playerPosition.PosY -= Gravity;
                        }
                        else
                        {
                            _playerPosition.PosY = BaseY;
                        }
                    }
                    else
                        _playerPosition.PosY = BaseY;
                    break;
                }
            case 1: // up
                {
                    _playerPosition.PosY += Gravity;
                    if (Gravity <= 0.0f)
                    {
                        JumpState = 2;
                    }
                    else
                    {
                        Gravity -= Jump_accell;   // 점프값이 점점 줄어들게
                    }
                    break;
                }
            case 2: // down
                {
                    _playerPosition.PosY -= Gravity;
                    if (_playerPosition.PosY > BaseY)
                    {
                        Gravity += Jump_accell;   // 점프값이 점점 늘어나게
                    }
                    else
                    {
                        _animator.SetBool("jump", false);
                        JumpState = 0;
                        _playerPosition.PosY = BaseY;
                    }
                    break;
                }
        }
    }
}
