using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(Healer))]
public class HealButton : HealthChanger
{
    [SerializeField] private Health _targetHealth;
    
    private Healer _healer;
    
    private void Awake()
    {
        Button = GetComponent<Button>();
        _healer = GetComponent<Healer>();
    }

    protected override void OnButtonClick()
    {
        _healer.Heal(_targetHealth);
    }
}
