using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class Enemy : MonoBehaviour
{
    private EnemyVision _enemyVision;
    private PlatformPatroller _platformPatroller;
    private PlayerChaser _playerChaser;
    
    private Mover _mover;
    private Attacker _attacker;
    private Health _health;

    private Transform _target;
    
    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _attacker = GetComponent<Attacker>();
        _health = GetComponent<Health>();
        _enemyVision = GetComponent<EnemyVision>();
        
        _platformPatroller = GetComponent<PlatformPatroller>();
        _playerChaser = GetComponent<PlayerChaser>();
    }
    
    private void Update()
    {
        if (_target != null)
        {
            _playerChaser.Chase();
            AttackIfPossible();
        }
        else
        {
            _platformPatroller.Patrol();
        }
    }

    private void OnEnable()
    {
        _enemyVision.PlayerSpotted += OnPlayerSpotted;
        _enemyVision.PlayerLost += OnPlayerLost;
        
        _health.Death += OnDeath;
    }

    private void OnDisable()
    {
        _enemyVision.PlayerSpotted -= OnPlayerSpotted;
        _enemyVision.PlayerLost -= OnPlayerLost;
        
        _health.Death -= OnDeath;
    }

    private void OnPlayerSpotted(Transform player)
    {
        _target = player;
        _playerChaser.SetTarget(_target);
    }

    private void OnPlayerLost()
    {
        _target = null;
        _platformPatroller.SetPatrolPoints();
    }

    private void AttackIfPossible()
    {
        float attackRangeSqr = _attacker.Range() * _attacker.Range();
        Vector3 direction = _target.transform.position - transform.position;
        
        if(Vector3.SqrMagnitude(direction) <= attackRangeSqr)
            _attacker.Attack();
    }

    private void OnDeath()
    {
        _platformPatroller.enabled = false;
        _mover.enabled = false;
        _attacker.enabled = false;
    }
}
