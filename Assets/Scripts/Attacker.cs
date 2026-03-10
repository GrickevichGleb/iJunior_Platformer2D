using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Visualizer))]
public class Attacker : MonoBehaviour
{
    private Visualizer _visualizer;

    private void Awake()
    {
        _visualizer = GetComponent<Visualizer>();
    }

    public void Attack()
    {
        _visualizer.OnAttack();
    }
}
