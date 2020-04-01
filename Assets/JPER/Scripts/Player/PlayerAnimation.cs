using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public bool IsDissovling = false;
    private Animator _animator;

    public Animator Animator => _animator;

    void Awake()
    {
        _animator = GetComponentInParent<Animator>();

        GetComponentInChildren<PlayerMove>().SetPlayerAnimation(this);
    }

    private void OffDissovle()
    {
        IsDissovling = false;
        Debug.Log("OffDissovle");
    }
}
