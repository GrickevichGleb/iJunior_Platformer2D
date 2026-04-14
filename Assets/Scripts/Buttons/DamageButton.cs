using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(Damager))]
public class DamageButton : HealthChanger
{
    [SerializeField] private Health _targetHealth;

    private Damager _damager;
    
    private void Awake()
    {
        Button = GetComponent<Button>();
        _damager = GetComponent<Damager>();
    }

    protected override void OnButtonClick()
    {
        _damager.DealDamage(_targetHealth);
    }
}
