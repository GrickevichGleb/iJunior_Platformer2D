using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{
    [SerializeField] private float _damageAmount = 20f;

    public void DealDamage(Health health, float multiplier = 1f)
    {
        health.Decrease(_damageAmount * multiplier);
    }
}
