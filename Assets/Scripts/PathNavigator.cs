using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNavigator : MonoBehaviour
{
    private const float ReachToleranceSqr = 0.01f;
    
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
    
    private void Update()
    {
        if (HasReachedWayPoint())
        {
            SetNextWayPointInd();
        }
    }
    
    private void SetNextWayPointInd()
    {
        int newIndex = 0;
        
        if (_isForwardMove)
        {
            newIndex = _currentWayPointInd + 1;

            if (newIndex == _wayPoints.Length)
                _isForwardMove = false;
        }

        if (_isForwardMove == false)
        {
            newIndex = _currentWayPointInd - 1;

            if (newIndex == 0)
                _isForwardMove = true;
        }

        _currentWayPointInd = newIndex;
    }

    private bool HasReachedWayPoint()
    {
        if (_currentWayPoint == null)
            return false;

        Vector3 direction = _currentWayPoint.position - transform.position;
        
        if (direction.sqrMagnitude <= ReachToleranceSqr)
            return true;

        return false;
    }
}
