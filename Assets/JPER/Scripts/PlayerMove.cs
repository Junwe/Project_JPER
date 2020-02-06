using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float MoveSpeed;
    public float JumpPower;
    public Rigidbody2D myRigidbody;

    public JoyStickButton leftMoveBtn;
    public JoyStickButton RightMoveBtn;
    // Start is called before the first frame update
    void Start()
    {
        leftMoveBtn.SetMoveEvent(LeftMove);
        RightMoveBtn.SetMoveEvent(RightMove);

    }

    // Update is called once per frame
    void Update()
    {

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
        myRigidbody.AddForce(Vector2.up * JumpPower);
    }


}
