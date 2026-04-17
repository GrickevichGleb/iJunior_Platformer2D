using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VampirismAbility : MonoBehaviour
{
    [SerializeField] private Health _health;
    
    [SerializeField] private Damager _damager;
    [SerializeField] private Healer _healer;

    [SerializeField] private SpriteRenderer _abilityVisual;
    [SerializeField] private float _range = 12f;
    [SerializeField] private float _duration = 6f;
    [SerializeField] private float _cooldownTime = 4f;

    private float _startTime;
    private bool _isActive = false;
    private bool _isReady = true;

    private void Start()
    {
        _abilityVisual.forceRenderingOff = true;
    }

    public void TryUseAbility()
    {
        if (_isActive || !_isReady)
            return;

        StartCoroutine(DrainHealth());
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
            //_damager.DealDamage(target);
            _healer.Heal(_health);

            yield return null;
        }

        _isActive = false;
        _abilityVisual.forceRenderingOff = true;

        StartCoroutine(Recharge());
    }

    private void SetSpriteVisible(bool visible)
    {
        
    }
}
