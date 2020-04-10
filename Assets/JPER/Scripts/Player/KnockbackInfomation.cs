using UnityEngine;
using UnityEngine.Events;
public class KnockbackInfomation
{
    // TODO
    // 아래 모든 public 필드들 모두 private 으로 교체
    private const float DEFAULT_VERTICAL_SPEED = 0.3f;
    private const float DEFAULT_HORIZONTAL_SPEED = 0.3f;
    private const float GRAVITY = 0.01f;                          // 중력 상수

    //private float verticalSpeed = DEFAULT_VERTICAL_SPEED;        // 초기 수직 이동속도
    //private float horizontalSpeed = DEFAULT_HORIZONTAL_SPEED;    // 초기 수평 이동속도
    private int direction;                                       // 넉백 방향. -1 : 왼쪽, 1 : 오른쪽.
    private float verticalAcceleration;                          // 수직 가속도
    private float horizontalAcceleration;                        // 수평 가속도
    private bool isUp;                                           // 넉백 상태 플래그. true : 올라가는중, false : 내려가는중.
    //private float groundY;                                       // 넉백 전에 플레이어가 딛고있던 땅의 높이

    private PlayerMove playerMove = null;
    private JumpInfomation jumpInfomation = null;
    private UnityEvent _knockEvent;

    public KnockbackInfomation(PlayerMove playerMove, JumpInfomation jumpInfomation,UnityEvent knockEvent)
    {
        this.playerMove = playerMove;
        this.jumpInfomation = jumpInfomation;
        this._knockEvent = knockEvent;
    }

    public bool KnuckbackFlag { get; private set; } = false;

    public void KnockbackProcess()
    {
        playerMove.AddPlayerPosition(new Vector2(horizontalAcceleration * direction, verticalAcceleration));
        verticalAcceleration -= GRAVITY; // 올라가는 속도 점점 느려지게 수직 가속도 감소.

        verticalAcceleration = Mathf.Clamp(verticalAcceleration, -0.35f, 10.35f);

        if (isUp == true && verticalAcceleration <= 0.0f)
        {
            isUp = false;
            jumpInfomation.JumpState = 2;
        }

        if (jumpInfomation.JumpState == 0)
            KnuckbackFlag = false;
    }

    public void OnKnockback(int direction, float verticalAcceleration = DEFAULT_VERTICAL_SPEED, float horizontalAcceleration = DEFAULT_HORIZONTAL_SPEED)
    {
        if (KnuckbackFlag == true)
            return;

        this.direction = direction;
        this.verticalAcceleration = verticalAcceleration;
        this.horizontalAcceleration = horizontalAcceleration;
        isUp = true;
        KnuckbackFlag = true;

        jumpInfomation.JumpState = 1;
        _knockEvent.Invoke();
    }
}
