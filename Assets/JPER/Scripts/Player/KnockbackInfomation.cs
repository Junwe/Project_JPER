public class KnockbackInfomation
{
    public const float DEFAULT_VERTICAL_SPEED = 0.3f;
    public const float DEFAULT_HORIZONTAL_SPEED = 0.3f;
    public const float GRAVITY = 0.01f;                          // 중력 상수

    public float verticalSpeed = DEFAULT_VERTICAL_SPEED;        // 초기 수직 이동속도
    public float horizontalSpeed = DEFAULT_HORIZONTAL_SPEED;    // 초기 수평 이동속도
    public int direction;                                       // 넉백 방향. -1 : 왼쪽, 1 : 오른쪽.
    public float verticalAcceleration;                          // 수직 가속도
    public float horizontalAcceleration;                        // 수평 가속도
    public bool isUp;                                           // 넉백 상태 플래그. true : 올라가는중, false : 내려가는중.
    public float groundY;                                       // 넉백 전에 플레이어가 딛고있던 땅의 높이
}
