using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthIndicator : MonoBehaviour
{
    [SerializeField] protected Health Health;
    
    private void OnEnable()
    {
        Health.Changed += OnHealthChanged;
        Health.Death += OnDeath;
    }
    
    private void OnDisable()
    {
        Health.Changed -= OnHealthChanged;
        Health.Death -= OnDeath;
    }

    protected virtual void OnHealthChanged() { }

    protected virtual void OnDeath()
    {
        gameObject.SetActive(false);
    }
}
