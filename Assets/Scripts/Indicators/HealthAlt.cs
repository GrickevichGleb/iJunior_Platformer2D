using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthAlt : ProgressBarValue
{
    private bool _isDead = false;
    
    public event Action Death;
    
    private void Start()
    {
        Current = Max;
    }

    public void Decrease(float amount)
    {
        if (_isDead)
            return;
        
        base.Decrease(amount);

        if (Current <= Min)
            Die();
    }
    
    private void Die()
    {
        Current = 0;
        _isDead = true;
        Death?.Invoke();
    }
}
