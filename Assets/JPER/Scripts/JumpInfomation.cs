using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpInfomation
{
    public float Gravity;     // 중력 점프 할때 중력을 높여줌
    public int JumpState = 0; // 점프 상태(0 : 정지 , 1 : 점프중, 2 : 떨어지는중) 
    public float BaseY = -4.32f;  // y 최소 좌표
    public  float Jump_speed = 0.2f;  // 점프속도
    public  float Jump_accell = 0.01f; // 점프가속
}
