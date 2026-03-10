using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class CoinsCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text _value;

    private int _coinsCollected = 0;
    private List<Coin> _coins;
    
    private void Start()
    {
        UpdateCounter();
        
        Coin[] coins = FindObjectsOfType<Coin>();
        _coins = coins.ToList();

        foreach (Coin coin in _coins)
        {
            coin.CoinCollected += OnCoinCollected;
        }
    }
    
    private void UpdateCounter()
    {
        _value.text = _coinsCollected.ToString();
    }

    private void OnCoinCollected(Coin coin)
    {
        _coinsCollected++;
        UpdateCounter();

        coin.CoinCollected -= OnCoinCollected;
        _coins.Remove(coin);
        
        Destroy(coin.gameObject);
    }
}
