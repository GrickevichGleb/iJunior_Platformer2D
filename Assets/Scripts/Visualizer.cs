using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visualizer : MonoBehaviour
{
    private const float FPTolerance = 0.001f;
    
    [SerializeField] private Animator _animator;

    private Transform _transform;

    private float _lastX = 1f;
    
    private int _isWalkingAnimFlag;
    private int _attackAnimTrigger;

    private void Awake()
    {
        _transform = gameObject.transform;
        _animator = GetComponent<Animator>();
        
        _isWalkingAnimFlag = Animator.StringToHash("isWalking");
        _attackAnimTrigger = Animator.StringToHash("attack");
    }

    public void OnMovement(float inputX)
    {
        if (Math.Abs(inputX - _lastX) > FPTolerance && inputX != 0f)
        {
            Vector3 newScale = _transform.localScale;
            newScale.x *= -1;

            _transform.localScale = newScale;

            _lastX = inputX;
        }
        
        _animator.SetBool(_isWalkingAnimFlag, (inputX != 0));
    }

    public void OnAttack()
    {
        _animator.SetTrigger(_attackAnimTrigger);
    }
}
