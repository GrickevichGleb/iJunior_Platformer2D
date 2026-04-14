using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class CoinsCounter : MonoBehaviour
{
    private int _coinsCollected = 0;

    public event Action<int> CoinCollected;
    
    private void Start()
    {
        CoinCollected?.Invoke(_coinsCollected);
    }

    public void Increment()
    {
        _coinsCollected++;
        CoinCollected?.Invoke(_coinsCollected);
    }
}
