using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Visualizer))]
public class Mover : MonoBehaviour
{
    private const float MarginFloat = 0.01f;
    
    [SerializeField] private float _moveSpeed = 4f;
    [SerializeField] private float _runSpeedMultiplier = 2f;
    [SerializeField] private float _moveForce = 6f;
    [SerializeField] private float _jumpForce = 1f;
    [SerializeField] private float _jumpCooldown = 1f;
    [SerializeField] private float _inAirMoveModifier = 0.5f;
    [SerializeField] private LayerMask _groundMask;

    public bool IsGrounded ()=> _isGrounded;

    private Transform _transform;
    private Rigidbody2D _rigidbody;
    private Collider2D _collider;
    private Visualizer _visualizer;

    private Vector2 _moveInput;

    private float _currentMoveSpeed;
    
    private bool _isGrounded = false;
    private bool _isJumping = false;
    private bool _canJump = true;

    private void Awake()
    {
        _currentMoveSpeed = _moveSpeed;
        
        _transform = GetComponent<Transform>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        _visualizer = GetComponent<Visualizer>();
    }

    private void FixedUpdate()
    {
        CheckGrounded();
        ClampInAirMovement();
        PerformJump();
        PerformMovement();
    }

    public void Move(Vector2 inputAxes)
    {
        _moveInput = inputAxes;
        
        if (inputAxes.x > 0f)
        {
            _transform.rotation = Quaternion.identity;
        }
        else if (inputAxes.x < 0f)
        {
            _transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
        
        _visualizer.OnMovement(inputAxes.x);
    }
    
    public void Jump()
    {
        if(_canJump)
            _isJumping = true;
    }

    public void SwitchRunning(bool running)
    {
        if (running)
            _currentMoveSpeed = _moveSpeed * _runSpeedMultiplier;
        else
            _currentMoveSpeed = _moveSpeed;
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

    private void PerformJump()
    {
        if (_isJumping)
        {
            if (!_isGrounded)
                return;
            
            _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
            _isJumping = false;
            
            StartCoroutine(JumpCooldown(_jumpCooldown));
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
            if (Mathf.Abs(_rigidbody.velocity.x) < _currentMoveSpeed)
                _rigidbody.AddForce(direction * _moveForce);
            
        }
        else
        {
            if(Mathf.Abs(_rigidbody.velocity.x) * _inAirMoveModifier < _currentMoveSpeed)
                _rigidbody.AddForce(direction.normalized * (_moveForce * _inAirMoveModifier)); 
        }
    }

    private void ClampInAirMovement()
    {
        if (!_isGrounded)
        {
            float clampedX = Mathf.Clamp(
                _rigidbody.velocity.x, -_currentMoveSpeed * _inAirMoveModifier,
                _currentMoveSpeed * _inAirMoveModifier);
            
            Vector2 inAirVelocity = new Vector2(clampedX, _rigidbody.velocity.y);

            _rigidbody.velocity = inAirVelocity;
        }
    }

    private void CheckGrounded()
    {
        float distance = MarginFloat;
        
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, distance, _groundMask);

        if (hit.collider != null)
            _isGrounded = true;
        else
            _isGrounded = false;
    }
    
    private IEnumerator JumpCooldown(float seconds)
    {
        _canJump = false;
        
        yield return new WaitForSeconds(seconds);

        _canJump = true;
    }
    
    private void OnDisable()
    {
        _moveInput = Vector2.zero;
        _rigidbody.velocity = Vector2.zero;
        
        _rigidbody.isKinematic = true;
        _collider.isTrigger = true;
    }
}
