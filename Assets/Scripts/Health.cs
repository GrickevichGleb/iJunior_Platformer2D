
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int _max = 100;

    public event Action Death;

    public int Max() => _max;
    public int Current() => _current;
    
    private int _current;
    private bool _isDead = false;

    private void Awake()
    {
        _current = _max;
    }

    public void TakeDamage(int damage)
    {
        if (_isDead)
            return;
        
        _current -= damage;

        if (_current <= 0)
            Die();
    }

    public void Heal(int amount)
    {
        _current = Mathf.Min(_current + amount, _max);
        
        Debug.Log($"Health now: {_current}");
    }

    private void Die()
    {
        _current = 0;
        _isDead = true;
        Death?.Invoke();
    }
    
}
