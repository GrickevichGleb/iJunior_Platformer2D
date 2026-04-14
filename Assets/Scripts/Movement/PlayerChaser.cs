using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChaser : MonoBehaviour
{
    private const float ReachToleranceSqr = 0.1f;
    
    private Mover _mover;
    
    private Transform _chaseTarget;
    
    private void Awake()
    {
        _mover = GetComponent<Mover>();
    }

    public void Chase()
    {
        MoveToTarget();
    }

    public void SetTarget(Transform target)
    {
        _chaseTarget = target;
        _mover.SwitchRunning(true);
    }
    
    private void MoveToTarget()
    {
        Vector2 simulatedInput = Vector2.zero;

        if (_chaseTarget.position.x > transform.position.x)
            simulatedInput.x = 1f;
        else
            simulatedInput.x = -1;

        _mover.Move(simulatedInput);
    }
}
