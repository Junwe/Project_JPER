using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public enum DirectionType
    {
        Left,
        Right
    }
    public enum PlayerMoveType
    {

        MoveInGround,
        JumppingUp,
        FallingDown,
        KnockBack
    }

    public float MoveSpeed;
    [Tooltip("Not use.")]
    public float JumpPower;
    public float JumpHeight;
    //public Rigidbody2D myRigidbody;

    public JoyStickButton leftMoveBtn;
    public JoyStickButton RightMoveBtn;

    private DirectionType currentDirection = DirectionType.Right;

    void Start()
    {
        leftMoveBtn.SetMoveEvent(LeftMove);
        RightMoveBtn.SetMoveEvent(RightMove);

        // 플레이어 방향 설정.
        if (currentDirection == DirectionType.Left)
            transform.localScale = new Vector3(-1f, 1f, 1f);
        else if (currentDirection == DirectionType.Right)
            transform.localScale = new Vector3(1f, 1f, 1f);
    }

    public void LeftMove()
    {
        transform.localPosition += Vector3.left * MoveSpeed * Time.deltaTime;
        transform.localScale = new Vector3(-1f, 1f, 1f);
    }

    public void RightMove()
    {
        transform.localPosition += Vector3.right * MoveSpeed * Time.deltaTime;
        transform.localScale = new Vector3(1f, 1f, 1f);
    }

    public void OnJump()
    {
        //myRigidbody.AddForce(Vector2.up * JumpPower);

    }

    private IEnumerator MoveToAir()
    {
        var playerPosition = transform.localPosition;

        var moveDirection = Vector3.zero;
        if (currentDirection == DirectionType.Left)
            moveDirection = Vector3.left;
        else if (currentDirection == DirectionType.Right)
            moveDirection = Vector3.right;

        while (playerPosition.y < JumpHeight)
        {
            
            yield return null;
        }
    }
}
