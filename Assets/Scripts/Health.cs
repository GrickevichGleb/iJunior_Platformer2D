
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    private bool _isDead = false;
    
    public event Action Changed;
    public event Action Death;
    
    [field:SerializeField] public int Max { get; private set; } = 100;
    public int Current { get; private set; }
    
    private void Awake()
    {
        Current = Max;
    }

    public void Decrease(int damage)
    {
        if (_isDead)
            return;
        
        Current -= damage;
        Current = Mathf.Clamp(Current, 0, Max);
        
        if (Current <= 0)
            Die();
        
        Changed?.Invoke();
    }

    public void Increase(int amount)
    {
        Current = Mathf.Min(Current + amount, Max);
        Current = Mathf.Clamp(Current, 0, Max);
        
        Changed?.Invoke();
    }

    private void Die()
    {
        Current = 0;
        _isDead = true;
        Death?.Invoke();
    }
    
}
