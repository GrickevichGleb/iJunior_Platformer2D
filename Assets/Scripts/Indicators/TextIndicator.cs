using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextIndicator : HealthIndicator
{
    [SerializeField] private TMP_Text _tmpText;
    
    private void Start()
    {
        OnHealthChanged();
    }
    
    protected override void OnHealthChanged()
    {
        _tmpText.text = $"{Health.Current} / {Health.Max}";
    }
}
