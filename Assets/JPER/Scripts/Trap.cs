﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Trap : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<PlayerMove>();
        if (player != null)
        {
            Debug.Log("Trap.OnTriggerEnter2D() : Player enter trigger detected.");
            int direction = Random.Range(-1, 1) < 0 ? -1 : 1;   // -1 : 왼쪽, 1 : 오른쪽
            player.OnKnockback(direction);
        }
    }
}