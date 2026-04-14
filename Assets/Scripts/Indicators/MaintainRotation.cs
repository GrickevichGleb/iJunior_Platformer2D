using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaintainRotation : MonoBehaviour
{
    [SerializeField] private Vector3 rotationAngles = Vector3.zero;

    private Mover _mover;
    
    private void Awake()
    {
        _mover = GetComponentInParent<Mover>();
    }

    private void OnEnable()
    {
        if (_mover != null)
            _mover.Moving += SetRotation;
    }

    private void OnDisable()
    {
        if (_mover != null)
            _mover.Moving -= SetRotation;
    }
    
    private void SetRotation(float _)
    {
        transform.rotation = Quaternion.Euler(rotationAngles);
    }
}
