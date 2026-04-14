using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{
    [SerializeField] private int _damageAmount = 20;

    public void DealDamage(Health health)
    {
        health.Decrease(_damageAmount);
    }
}
