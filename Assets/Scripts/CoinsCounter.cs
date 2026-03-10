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

    private void Start()
    {
        UpdateCounter();
    }

    public void Increment()
    {
        _coinsCollected++;
        UpdateCounter();
    }
    
    private void UpdateCounter()
    {
        _value.text = _coinsCollected.ToString();
    }
}
