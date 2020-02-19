public class KnockbackInfomation
{
    public float verticalSpeed = 1;    // 초기 수직 이동속도
    public float horizontalSpeed = 1;  // 초기 수평 이동속도
    public float gravity = 0.1f;  // 중력 (수직 가속도 감소치)
    public float fraction = 0.1f; // 마찰력 (수평 가속도 감소치)
    public int direction;  // 넉백 방향. -1 : 왼쪽, 1 : 오른쪽.
    public float verticalAcceleration;
    public float horizontalAcceleration;
    public bool isUp;  // 넉백 상태 플래그. true : 올라가는중, false : 내려가는중.
    public float groundY;  // 넉백 전에 플레이어가 딛고있던 땅의 높이
}
