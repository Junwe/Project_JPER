using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float MoveSpeed;
    [Tooltip("Not use.")]
    public float JumpPower;
    public float JumpHeight;
    //public Rigidbody2D myRigidbody;

    public JoyStickButton leftMoveBtn;
    public JoyStickButton RightMoveBtn;

    private Vector3 lastMoveSpeed = Vector3.zero;
    private bool isOnGround = true;

    void Start()
    {
        leftMoveBtn.SetMoveEvent(LeftMove);
        RightMoveBtn.SetMoveEvent(RightMove);

        transform.localPosition = FindGround();
    }

#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
            LeftMove();
        else if (Input.GetKey(KeyCode.RightArrow))
            RightMove();
        else if (Input.GetKey(KeyCode.LeftArrow) == false &&
                 Input.GetKey(KeyCode.RightArrow) == false)
            lastMoveSpeed = Vector3.zero;

        if (Input.GetKeyDown(KeyCode.Space))
            OnJump();
    }
#endif

    public void LeftMove()
    {
        lastMoveSpeed = Vector3.left * MoveSpeed * Time.deltaTime;
        transform.localPosition += lastMoveSpeed;
        transform.localScale = new Vector3(-1f, 1f, 1f);

        //if (FindGround().y < transform.localPosition.y)
        //    StartCoroutine(Falling());
    }

    public void RightMove()
    {
        lastMoveSpeed = Vector3.right * MoveSpeed * Time.deltaTime;
        transform.localPosition += lastMoveSpeed;
        transform.localScale = new Vector3(1f, 1f, 1f);

        //if (FindGround().y < transform.localPosition.y)
        //    StartCoroutine(Falling());
    }

    public void OnJump()
    {
        if (isOnGround == false)
            return;

        //myRigidbody.AddForce(Vector2.up * JumpPower);
        StartCoroutine(JumpUp());
    }

    private IEnumerator JumpUp()
    {
        float maxJumpHeight = transform.localPosition.y + JumpHeight;
        Vector3 jumpSpeed = Vector3.up * MoveSpeed * Time.deltaTime;

        while (transform.localPosition.y < maxJumpHeight)
        {
            transform.localPosition += lastMoveSpeed + jumpSpeed;

            yield return null;
        }

        yield return Falling();
    }

    public IEnumerator Falling()
    {
        Vector3 fallSpeed = Vector3.down * MoveSpeed * Time.deltaTime;
        Vector3 groundLocation = FindGround();

        while (transform.localPosition.y > groundLocation.y)
        {
            transform.localPosition += lastMoveSpeed + fallSpeed;
            yield return null;
            groundLocation = FindGround();
        }

        transform.localPosition =
            new Vector3(transform.localPosition.x, groundLocation.y, transform.localPosition.z);
    }

    private Vector2 FindGround()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.localPosition, Vector2.down);

#if UNITY_EDITOR
        Debug.DrawRay(transform.localPosition, Vector2.down, Color.red);
#endif

        return hit.point;
    }
}
