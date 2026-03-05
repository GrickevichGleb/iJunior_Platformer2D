using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Mover : MonoBehaviour
{
    private const float MarginFloat = 0.01f;
    
    [SerializeField] private float _moveSpeed = 4f;
    [SerializeField] private float _moveForce = 6f;
    [SerializeField] private float _jumpForce = 1f;
    [SerializeField] private float _inAirMoveModifier = 0.5f;
    [SerializeField] private LayerMask _groundMask;
    
    private Rigidbody2D _rigidbody;
    private Visualizer _visualizer;

    private Vector2 _moveInput;

    private bool _isGrounded = false;
    private bool _isJumping = false;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _visualizer = GetComponent<Visualizer>();
    }

    private void FixedUpdate()
    {
        CheckGrounded();
        
        ClampInAirMovement();
        PerformJump();
        PerformMovement();
    }

    private void CheckGrounded()
    {
        _isGrounded = IsGrounded();
    }

    public void Move(Vector2 inputAxes)
    {
        _moveInput = inputAxes;
        
        _visualizer.OnMovement(inputAxes.x);
    }

    private void PerformMovement()
    {
        if (_moveInput == Vector2.zero)
        {
            ApplyResistance();
            return;
        }
        
        ApplyMoveForce(_moveInput);
    }

    public void Jump()
    {
        _isJumping = true;
    }

    private void PerformJump()
    {
        if (_isJumping)
        {
            if (!_isGrounded)
                return;
            
            _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
            _isJumping = false;
        }
    }

    private void ApplyResistance()
    {
        if (_rigidbody.velocity.sqrMagnitude > MarginFloat && _isGrounded)
        {
            Vector2 resistance = -_rigidbody.velocity.normalized;
            ApplyMoveForce(resistance);
        }
    }

    private void ApplyMoveForce(Vector2 inputAxes)
    {
        Vector2 direction = new Vector2(inputAxes.x, 0f);;

        if (_isGrounded)
        {
            if (Mathf.Abs(_rigidbody.velocity.x) < _moveSpeed)
                _rigidbody.AddForce(direction * _moveForce);
            
        }
        else
        {
            if(Mathf.Abs(_rigidbody.velocity.x) * _inAirMoveModifier < _moveSpeed)
                _rigidbody.AddForce(direction.normalized * (_moveForce * _inAirMoveModifier)); 
        }
    }

    private void ClampInAirMovement()
    {
        if (!_isGrounded)
        {
            float clampedX = Mathf.Clamp(
                _rigidbody.velocity.x, -_moveSpeed * _inAirMoveModifier,
                _moveSpeed * _inAirMoveModifier);
            
            Vector2 inAirVelocity = new Vector2(clampedX, _rigidbody.velocity.y);

            _rigidbody.velocity = inAirVelocity;
        }
    }

    private bool IsGrounded()
    {
        float distance = MarginFloat;
        
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, distance, _groundMask);
        
        if (hit.collider != null)
        {
            return true;
        }
        
        return false;
    }
}
