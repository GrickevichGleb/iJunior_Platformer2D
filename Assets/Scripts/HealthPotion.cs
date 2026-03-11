using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    [SerializeField] private int _healValue = 50;

    public void Use(Health health)
    {
        health.Heal(_healValue);
    }
}
