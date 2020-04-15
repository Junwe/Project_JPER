using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Trap : MonoBehaviour, ITrapExecute
{
    public float verticalAccel = .1f;
    public float horizontalAccel = .1f;

    public enum KnockbackType
    {
        Random = 0,
        Relation,
        Left,
        Right
    }

    [SerializeField]
    private KnockbackType knockbackDirection;

    public void ValueCopy(Trap spike)
    {
        verticalAccel = spike.verticalAccel;
        horizontalAccel = spike.horizontalAccel;
        knockbackDirection = spike.knockbackDirection;
    }

    public void Execute(PlayerMove targetPlayer)
    {
        switch (knockbackDirection)
        {
            case KnockbackType.Random:
                {
                    int direction = Random.Range(-1, 1) < 0 ? -1 : 1;
                    targetPlayer.KnockbackInfomation.OnKnockback(direction, verticalAccel, horizontalAccel);
                    break;
                }
            case KnockbackType.Relation:
                {
                    int direction = transform.position.x - targetPlayer.transform.position.x > 0 ? -1 : 1;
                    targetPlayer.KnockbackInfomation.OnKnockback(direction, verticalAccel, horizontalAccel);
                    break;
                }
            case KnockbackType.Left:
                {
                    int direction = -1;
                    targetPlayer.KnockbackInfomation.OnKnockback(direction, verticalAccel, horizontalAccel);
                    break;
                }
            case KnockbackType.Right:
                {
                    int direction = 1;
                    targetPlayer.KnockbackInfomation.OnKnockback(direction, verticalAccel, horizontalAccel);
                    break;
                }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        var player = collision.GetComponent<PlayerMove>();
        if (player != null && player.KnockbackInfomation.KnockbackFlag == false)
        {
            Debug.Log("Trap.OnTriggerStay2D() : [" + GetInstanceID() + "] Invoke from OnTriggerStay2D().");
            Execute(player);
        }
    }
}
