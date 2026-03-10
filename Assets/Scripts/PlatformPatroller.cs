using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformPatroller : MonoBehaviour
{
    private const float ReachToleranceSqr = 0.1f;
    private const float PlatformEdgeDistance = 0.5f;
    
    [SerializeField] private LayerMask _groundMask;
    
    private Mover _mover;
    
    private Vector3[] _wayPoints;
    private Vector3 _currentWayPoint;
    private int _currentWayPointInd = 0;
    
    private bool _isMovementPaused = false;
    private bool _isGrounded = false;
    
    private void Awake()
    {
        _mover = GetComponent<Mover>();
    }

    private void Start()
    {
        SetPatrolPoints();
    }
    
    private void Update()
    {
        if (_isGrounded == false && _mover.IsGrounded() == true)
            SetPatrolPoints();

        _isGrounded = _mover.IsGrounded();

        if (_wayPoints == null)
            return;
        
        if (HasReachedWayPoint())
        {
            StartCoroutine(StopForSecondsCoroutine(2f));
            SetNextWayPointInd();
        }
        
        MoveToWayPoint();
    }
    
    private void MoveToWayPoint()
    {
        Vector2 simulatedInput = Vector2.zero;

        if (_currentWayPoint.x > transform.position.x)
            simulatedInput.x = 1f;
        else
            simulatedInput.x = -1;

        if (_isMovementPaused)
            simulatedInput = Vector2.zero;
        
        _mover.Move(simulatedInput);
    }
    
    private void SetNextWayPointInd()
    {
        int newIndex = (_currentWayPointInd + 1) % _wayPoints.Length;
        
        _currentWayPointInd = newIndex;
        _currentWayPoint = _wayPoints[newIndex];
    }

    private bool HasReachedWayPoint()
    {
        Vector3 direction = _currentWayPoint - transform.position;

        if (direction.sqrMagnitude <= ReachToleranceSqr)
            return true;

        return false;
    }

    private IEnumerator StopForSecondsCoroutine(float seconds)
    {
        _isMovementPaused = true;
        
        yield return new WaitForSeconds(seconds);

        _isMovementPaused = false;
    }

    private void SetPatrolPoints()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, ReachToleranceSqr, _groundMask);
        
        if (hit.collider != null)
        {
            var bounds = hit.collider.bounds;

            Vector3 topCenter = new Vector3(
                bounds.center.x,
                bounds.center.y + bounds.extents.y,
                bounds.center.z);

            Vector3 leftTopEdge = new Vector3(
                topCenter.x - bounds.extents.x + PlatformEdgeDistance,
                topCenter.y,
                topCenter.z);
            
            Vector3 rightTopEdge = new Vector3(
                topCenter.x + bounds.extents.x - PlatformEdgeDistance,
                topCenter.y,
                topCenter.z);

            _wayPoints = new Vector3[] { leftTopEdge, rightTopEdge };
            _currentWayPoint = _wayPoints[0];
        }
    }
}
