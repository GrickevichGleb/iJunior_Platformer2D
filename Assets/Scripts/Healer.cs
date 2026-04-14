using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healer : MonoBehaviour
{
    [SerializeField] private int _healAmount = 20;

    public void Heal(Health health)
    {
        health.Increase(_healAmount);
    }
}
