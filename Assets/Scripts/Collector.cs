using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Collector : MonoBehaviour
{
    [SerializeField] private CoinsCounter _coinsCounter;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Coin coin))
        {
            _coinsCounter.Increment();
            
            Destroy(coin.gameObject);
        }
    }
}
