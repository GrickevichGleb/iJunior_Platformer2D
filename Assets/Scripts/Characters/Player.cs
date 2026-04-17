using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(Attacker))]
[RequireComponent(typeof(Abilities))]
public class Player : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    
    private Health _health;
    private Mover _mover;
    private Attacker _attacker;
    private Abilities _abilities;

    private void Awake()
    {
        _health = GetComponent<Health>();
        _mover = GetComponent<Mover>();
        _attacker = GetComponent<Attacker>();
        _abilities = GetComponent<Abilities>();
    }

    private void OnEnable()
    {
        _inputReader.InputAxesChanged += OnInputAxes;
        _inputReader.JumpKeyPressed += OnJumpKeyPressed;
        _inputReader.AttackKeyPressed += OnAttackKeyPressed;
        _inputReader.VampirismKeyPressed += OnVampirismKeyPressed;

        _health.Death += OnDeath;
    }

    private void OnDisable()
    {
        _inputReader.InputAxesChanged -= OnInputAxes;
        _inputReader.JumpKeyPressed -= OnJumpKeyPressed;
        _inputReader.AttackKeyPressed -= OnAttackKeyPressed;
        _inputReader.VampirismKeyPressed -= OnVampirismKeyPressed;
        
        _health.Death -= OnDeath;
    }
    

    private void OnInputAxes(Vector2 axes)
    {
        _mover.Move(axes);
    }

    private void OnJumpKeyPressed()
    {
        _mover.Jump();
    }

    private void OnAttackKeyPressed()
    {
        _attacker.Attack();
    }

    private void OnVampirismKeyPressed()
    {
        _abilities.UseVampirism();
    }
    
    private void OnDeath()
    {
        _attacker.enabled = false;
        _mover.enabled = false;
    }
}
