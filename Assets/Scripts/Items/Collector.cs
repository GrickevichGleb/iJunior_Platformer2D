using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Collector : MonoBehaviour
{
    [SerializeField] private CoinsCounter _coinsCounter;


    private Health _health;
    
    private void Awake()
    {
        _health = GetComponent<Health>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Coin coin))
        {
            _coinsCounter.Increment();
            
            Destroy(coin.gameObject);
        }

        if (other.TryGetComponent(out HealthPotion healthPotion))
        {
            healthPotion.Use(_health);
            
            Destroy(healthPotion.gameObject);
        }
    }
}
