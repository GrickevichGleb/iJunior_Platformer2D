using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float _spotDistance = 8f;
    [SerializeField] private float _chaseMaxRange = 12f;
    
    private PlatformPatroller _platformPatroller;
    private PlayerChaser _playerChaser;
    
    private Mover _mover;
    private Attacker _attacker;
    private Health _health;

    private Collider2D _collider;
    
    private Transform _target;
    
    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        
        _mover = GetComponent<Mover>();
        _attacker = GetComponent<Attacker>();
        _health = GetComponent<Health>();
        
        _platformPatroller = GetComponent<PlatformPatroller>();
        _playerChaser = GetComponent<PlayerChaser>();
    }

    private void FixedUpdate()
    {
        CheckForPlayerInSight();
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
        _health.Death += OnDeath;
    }

    private void OnDisable()
    {
        _health.Death -= OnDeath;
    }

    private void AttackIfPossible()
    {
        float attackRangeSqr = _attacker.Range() * _attacker.Range();
        Vector3 direction = _target.transform.position - transform.position;
        
        if(Vector3.SqrMagnitude(direction) <= attackRangeSqr)
            _attacker.Attack();
    }

    private void CheckForPlayerInSight()
    {
        RaycastHit2D[] hits = 
            Physics2D.RaycastAll(_collider.bounds.center, transform.right, _spotDistance);

        foreach (var hit in hits)
        {
            if(hit.collider.gameObject == this.gameObject)
                continue;

            if (hit.collider.gameObject.TryGetComponent(out Player player))
            {
                _target = player.transform;
                _playerChaser.SetTarget(_target);
                
                return;
            }
        }

        if (_target == null)
            return;

        _target = null;
        _platformPatroller.SetPatrolPoints();
    }

    private void OnDeath()
    {
        _platformPatroller.enabled = false;
        _mover.enabled = false;
        _attacker.enabled = false;
    }
}
