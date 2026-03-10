using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visualizer : MonoBehaviour
{
    private const string isWalking = nameof(isWalking);
    private const string attack = nameof(attack);
    
    [SerializeField] private Animator _animator;
    
    private int _isWalkingAnimFlag;
    private int _attackAnimTrigger;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        
        _isWalkingAnimFlag = Animator.StringToHash(isWalking);
        _attackAnimTrigger = Animator.StringToHash(attack);
    }

    public void OnMovement(float inputX)
    {
        _animator.SetBool(_isWalkingAnimFlag, (inputX != 0));
    }

    public void OnAttack()
    {
        _animator.SetTrigger(_attackAnimTrigger);
    }
}
