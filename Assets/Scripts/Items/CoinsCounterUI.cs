using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinsCounterUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _textField;
    [SerializeField] private CoinsCounter _coinsCounter;
    
    private void OnEnable()
    {
        _coinsCounter.CoinCollected += UpdateCounter;
    }

    private void OnDisable()
    {
        _coinsCounter.CoinCollected -= UpdateCounter;
    }
    
    private void UpdateCounter(int coins)
    {
        _textField.text = coins.ToString();
    }
}
