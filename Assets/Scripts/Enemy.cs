using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float _spotDistance = 8f;
    [SerializeField] private float _chaseMaxRange = 12f;
    
    private Mover _mover;
    private PlatformPatroller _platformPatroller;
    private Attacker _attacker;
    private Health _health;

    private Collider2D _collider;
    
    private Transform _target;
    
    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        
        _mover = GetComponent<Mover>();
        _platformPatroller = GetComponent<PlatformPatroller>();
        _attacker = GetComponent<Attacker>();
        _health = GetComponent<Health>();
    }

    private void FixedUpdate()
    {
        CheckForPlayerInSight();
    }

    private void Update()
    {
        if(_target != null)
            _platformPatroller.MoveTowards(_target.position);
    }

    private void OnEnable()
    {
        _health.Death += OnDeath;
    }

    private void OnDisable()
    {
        _health.Death -= OnDeath;
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
                _attacker.Attack();
                _target = player.transform;
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
