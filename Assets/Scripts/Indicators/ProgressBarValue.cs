using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBarValue : MonoBehaviour
{
    public event Action Changed;
    
    [field:SerializeField] public float Max { get; protected set; } = 100f;
    [field:SerializeField] public float Min { get; protected set; } = 0f;
    
    public float Current { get; protected set; }

    public void Increase(float amount)
    {
        Current = Mathf.Clamp(Current + amount, Min, Max);
        
        Changed?.Invoke();
    }

    public void Decrease(float amount)
    {
        Current = Mathf.Clamp(Current - amount, Min, Max);
        
        Changed?.Invoke();
    }
}
