using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : MonoBehaviour
{
    private Visualizer _visualizer;

    private void Start()
    {
        _visualizer = GetComponent<Visualizer>();
    }

    public void Attack()
    {
        _visualizer.OnAttack();
    }
}
