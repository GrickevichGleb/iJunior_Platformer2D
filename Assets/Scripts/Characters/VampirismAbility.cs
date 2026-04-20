using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VampirismAbility : MonoBehaviour
{
    [SerializeField] private Health _health;
    
    [SerializeField] private Damager _damager;
    [SerializeField] private Healer _healer;

    [SerializeField] private LayerMask _enemiesLayerMask;
    [SerializeField] private SpriteRenderer _abilityVisual;
    [SerializeField] private float _range = 6f;
    [SerializeField] private float _duration = 6f;
    [SerializeField] private float _cooldownTime = 4f;

    private ContactFilter2D _contactFilter;
    
    private float _startTime;
    private bool _isActive = false;
    private bool _isReady = true;

    private Collider2D[] _enemiesWithinRange = new Collider2D[1];

    private Collider2D _targetCollider;
    private Health _targetHealth;
    
    private void Start()
    {
        _abilityVisual.forceRenderingOff = true;
        _contactFilter.SetLayerMask(_enemiesLayerMask);
    }

    public void TryUseAbility()
    {
        if (_isActive || !_isReady)
            return;

        StartCoroutine(DrainHealth());
    }
    
    private void GetTarget()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(_abilityVisual.transform.position, _range, _enemiesLayerMask);
        if (enemies.Length > 0)
        {
            if (_targetCollider != enemies[0])
                _targetHealth = enemies[0].GetComponent<Health>();
            
            _targetCollider = enemies[0];
        }
        else
        {
            _targetCollider = null;
            _targetHealth = null;
        }
    }
    
    private IEnumerator Recharge()
    {
        _isReady = false;

        yield return new WaitForSeconds(_cooldownTime);

        _isReady = true;
    }
    
    private IEnumerator DrainHealth()
    {
        Debug.Log("Vampirism");
        
        _abilityVisual.forceRenderingOff = false;
        _startTime = Time.time;
        _isActive = true;
        
        while (Time.time - _startTime < _duration)
        {
            GetTarget();
            
            if (_targetHealth != null)
            {
                _damager.DealDamage(_targetHealth, Time.deltaTime / _duration);
                _healer.Heal(_health, Time.deltaTime / _duration);
            }

            yield return null;
        }

        _isActive = false;
        _abilityVisual.forceRenderingOff = true;

        StartCoroutine(Recharge());
    }
}
