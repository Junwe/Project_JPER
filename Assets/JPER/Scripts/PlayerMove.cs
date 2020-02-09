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

    private BoxCollider2D boxCollider = null;
    private Vector3 lastMoveSpeed = Vector3.zero;
    private bool isInAir = false;
    private bool isJumpUp = false;

    void Start()
    {
        leftMoveBtn.SetMoveEvent(LeftMove);
        RightMoveBtn.SetMoveEvent(RightMove);

        boxCollider = GetComponent<BoxCollider2D>();

        transform.localPosition = FindGround();
        StartCoroutine(FallingCheck());
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

        if (FindGround().y < transform.localPosition.y)
            isInAir = true;
    }

    public void RightMove()
    {
        lastMoveSpeed = Vector3.right * MoveSpeed * Time.deltaTime;
        transform.localPosition += lastMoveSpeed;
        transform.localScale = new Vector3(1f, 1f, 1f);

        if (FindGround().y < transform.localPosition.y)
            isInAir = true;
    }

    public void OnJump()
    {
        if (isInAir == true)
            return;

        //myRigidbody.AddForce(Vector2.up * JumpPower);
        StartCoroutine(JumpUp());
    }

    private IEnumerator JumpUp()
    {
        float maxJumpHeight = transform.localPosition.y + JumpHeight;
        Vector3 jumpSpeed = Vector3.up * MoveSpeed * Time.deltaTime;

        isJumpUp = true;
        while (transform.localPosition.y < maxJumpHeight)
        {
            transform.localPosition += lastMoveSpeed + jumpSpeed;

            yield return null;
        }
        isJumpUp = false;
    }

    private IEnumerator FallingCheck()
    {
        while (true)
        {
            if (isInAir == true && isJumpUp == false)
                yield return Falling();

            yield return null;
        }
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
        isInAir = false;
    }

    private Vector2 FindGround()
    {
        Vector2 leftPoint = 
            transform.localPosition + new Vector3(-boxCollider.size.x * 0.5f, 0, 0);
        Vector2 rightPoint = 
            transform.localPosition + new Vector3(boxCollider.size.x * 0.5f, 0, 0);

        RaycastHit2D hitLeft = Physics2D.Raycast(leftPoint, Vector2.down);
        RaycastHit2D hitRight = Physics2D.Raycast(rightPoint, Vector2.down);

#if UNITY_EDITOR
        Debug.DrawRay(leftPoint, Vector2.down, Color.red);
        Debug.DrawRay(rightPoint, Vector2.down, Color.red);
#endif

        return hitLeft.distance < hitRight.distance ? hitLeft.point : hitRight.point;
    }
}
