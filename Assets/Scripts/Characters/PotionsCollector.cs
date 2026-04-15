using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class PotionsCollector : MonoBehaviour
{
    private Health _health;
    
    private void Awake()
    {
        _health = GetComponent<Health>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out HealthPotion healthPotion))
        {
            healthPotion.Use(_health);
            
            Destroy(healthPotion.gameObject);
        }
    }
}
