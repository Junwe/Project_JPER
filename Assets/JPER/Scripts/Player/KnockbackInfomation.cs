using UnityEngine;
using UnityEngine.Events;
public class KnockbackInfomation
{
    private const float DEFAULT_VERTICAL_SPEED = 0.3f;
    private const float DEFAULT_HORIZONTAL_SPEED = 0.3f;
    private const float GRAVITY = 0.01f;                         // 중력 상수

    private int direction;                                       // 넉백 방향. -1 : 왼쪽, 1 : 오른쪽.
    private float verticalAcceleration;                          // 수직 가속도
    private float horizontalAcceleration;                        // 수평 가속도
    //private bool isUp;                                           // 넉백 상태 플래그. true : 올라가는중, false : 내려가는중.

    private PlayerMove playerMove = null;
    private JumpInfomation jumpInfomation = null;
    private UnityEvent _knockEvent;

    public KnockbackInfomation(PlayerMove playerMove, JumpInfomation jumpInfomation, UnityEvent knockEvent)
    {
        this.playerMove = playerMove;
        this.jumpInfomation = jumpInfomation;
        this._knockEvent = knockEvent;
    }

    public bool KnockbackFlag { get; private set; } = false;

    public void KnockbackProcess()
    {
        playerMove.AddPlayerPosition(new Vector2(horizontalAcceleration * direction, verticalAcceleration));
        verticalAcceleration -= GRAVITY; // 올라가는 속도 점점 느려지게 수직 가속도 감소.

        verticalAcceleration = Mathf.Clamp(verticalAcceleration, -0.35f, 0.35f);

        if (jumpInfomation.JumpState == JumpInfomation.JumpStateType.Up && verticalAcceleration <= 0.0f)
            jumpInfomation.JumpState = JumpInfomation.JumpStateType.Down;

        if (jumpInfomation.JumpState == JumpInfomation.JumpStateType.None)
        {
            KnockbackFlag = false;
        }
    }

    public void OnKnockback(int direction, float verticalAcceleration = DEFAULT_VERTICAL_SPEED, float horizontalAcceleration = DEFAULT_HORIZONTAL_SPEED)
    {
        if (KnockbackFlag == true)
            return;

        this.direction = direction;
        this.verticalAcceleration = verticalAcceleration;
        this.horizontalAcceleration = horizontalAcceleration;
        //isUp = true;
        KnockbackFlag = true;

        jumpInfomation.JumpState = JumpInfomation.JumpStateType.Up;

        playerMove.particleDust.Stop();
        _knockEvent.Invoke();
    }
}
