using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    [SerializeField] private float _spotDistance = 8f;

    public event Action<Transform> PlayerSpotted;
    public event Action PlayerLost;
    
    private Collider2D _collider;

    private Transform _targetTransform;
    
    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
    }

    private void FixedUpdate()
    {
        CheckForPlayerInSight();
    }

    private void CheckForPlayerInSight()
    {
        RaycastHit2D[] hits = 
            Physics2D.RaycastAll(_collider.bounds.center, transform.right, _spotDistance);

        foreach (var hit in hits)
        {
            if(hit.collider.gameObject == this.gameObject)
                continue;

            if (hit.collider.gameObject.TryGetComponent(out Player player))
            {
                if (_targetTransform == null)
                {
                    _targetTransform = player.transform;
                    PlayerSpotted?.Invoke(_targetTransform);
                }
                
                return;
            }
        }

        if (_targetTransform == null)
            return;

        _targetTransform = null;
        PlayerLost?.Invoke();
    }
}
