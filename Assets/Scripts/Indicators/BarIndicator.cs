using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarIndicator : HealthIndicator
{
    [SerializeField] private Image _fillerImage; 

    private void Start()
    {
        OnHealthChanged();
    }
    
    protected override void OnHealthChanged()
    {
        _fillerImage.fillAmount = Convert.ToSingle(Health.Current) / Convert.ToSingle(Health.Max);
    }
}
