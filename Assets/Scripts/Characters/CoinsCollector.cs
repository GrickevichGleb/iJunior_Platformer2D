using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CoinsCounter))]
public class CoinsCollector : MonoBehaviour
{
    private CoinsCounter _coinsCounter;

    private void Awake()
    {
        _coinsCounter = GetComponent<CoinsCounter>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Coin coin))
        {
            _coinsCounter.Increment();
            
            Destroy(coin.gameObject);
        }
    }
}
