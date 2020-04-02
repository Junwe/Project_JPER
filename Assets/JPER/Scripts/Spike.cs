using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Spike : MonoBehaviour, ITrapExecute
{
    public float VERTICAL_ACCEL = .1f;
    public float HORIZONTAL_ACCEL = .1f;

    public enum KnockbackType
    {
        Random = 0,
        Relation,
        Left,
        Right
    }

    [SerializeField]
    private KnockbackType knockbackDirection;

    public void Execute(PlayerMove targetPlayer)
    {
        switch (knockbackDirection)
        {
            case KnockbackType.Random:
                {
                    int direction = Random.Range(-1, 1) < 0 ? -1 : 1;
                    targetPlayer.KnockbackInfomation.OnKnockback(direction, VERTICAL_ACCEL, HORIZONTAL_ACCEL);
                    break;
                }
            case KnockbackType.Relation:
                {
                    int direction = transform.position.x - targetPlayer.transform.position.x > 0 ? -1 : 1;
                    targetPlayer.KnockbackInfomation.OnKnockback(direction, VERTICAL_ACCEL, HORIZONTAL_ACCEL);
                    break;
                }
            case KnockbackType.Left:
                {
                    int direction = -1;
                    targetPlayer.KnockbackInfomation.OnKnockback(direction, VERTICAL_ACCEL, HORIZONTAL_ACCEL);
                    break;
                }
            case KnockbackType.Right:
                {
                    int direction = 1;
                    targetPlayer.KnockbackInfomation.OnKnockback(direction, VERTICAL_ACCEL, HORIZONTAL_ACCEL);
                    break;
                }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<PlayerMove>();
        if (player != null)
            Execute(player);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        var player = collision.GetComponent<PlayerMove>();
        if (player != null && player.KnockbackInfomation.KnuckbackFlag == false)
            Execute(player);
    }
}
