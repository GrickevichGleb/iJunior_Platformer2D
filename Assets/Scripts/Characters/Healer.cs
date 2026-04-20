using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healer : MonoBehaviour
{
    [SerializeField] private float _healAmount = 20;

    public void Heal(Health health, float multiplier)
    {
        health.Increase(_healAmount * multiplier);
    }
}
