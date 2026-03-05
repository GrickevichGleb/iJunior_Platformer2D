using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject _patrolPath;
    
    private Mover _mover;

    private WayPoint[] _wayPoints;
    
    private Transform _currentWayPoint;
    private int _currentWayPointInd = 0;
    private bool _isForwardMove = true;
    
    private void Awake()
    {
        _mover = GetComponent<Mover>();
    }

    private void Start()
    {
        _wayPoints = _patrolPath.GetComponentsInChildren<WayPoint>();
    }
    
}
