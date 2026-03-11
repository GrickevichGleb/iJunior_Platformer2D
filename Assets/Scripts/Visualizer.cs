using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Animator))]
public class Visualizer : MonoBehaviour
{
    private const string isWalking = nameof(isWalking);
    private const string attack = nameof(attack);
    private const string death = nameof(death);
    
    private Animator _animator;
    private Health _health;
    
    private int _isWalkingAnimFlag;
    private int _attackAnimTrigger;
    private int _deathAnimTrigger;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _health = GetComponent<Health>();
        
        _isWalkingAnimFlag = Animator.StringToHash(isWalking);
        _attackAnimTrigger = Animator.StringToHash(attack);
        _deathAnimTrigger = Animator.StringToHash(death);
    }

    private void OnEnable()
    {
        _health.Death += OnDeath;
    }

    private void OnDisable()
    {
        _health.Death -= OnDeath;
    }

    public void OnMovement(float inputX)
    {
        _animator.SetBool(_isWalkingAnimFlag, (inputX != 0));
    }

    public void OnAttack()
    {
        _animator.SetTrigger(_attackAnimTrigger);
    }

    private void OnDeath()
    {
        Debug.Log("Play death anim");
        _animator.SetTrigger(_deathAnimTrigger);
    }
}
