using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(Attacker))]
public class Player : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private KeyCode _jumpKey = KeyCode.UpArrow;
    [SerializeField] private KeyCode _attackKey = KeyCode.Space;
    
    private Mover _mover;
    private Attacker _attacker;
    
    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _attacker = GetComponent<Attacker>();
    }

    private void OnEnable()
    {
        _inputReader.InputAxesChanged += OnInputAxes;
        _inputReader.KeyPressed += OnKeyPressed;
    }

    private void OnDisable()
    {
        _inputReader.InputAxesChanged -= OnInputAxes;
        _inputReader.KeyPressed -= OnKeyPressed;
    }
    

    private void OnInputAxes(Vector2 axes)
    {
        _mover.Move(axes);
    }

    private void OnKeyPressed(KeyCode keyCode)
    {
        if(keyCode == _jumpKey)
            _mover.Jump();
        
        if(keyCode == _attackKey)
            _attacker.Attack();
    }
}
