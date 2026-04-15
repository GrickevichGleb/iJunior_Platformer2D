using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    private const float MarginFloat = 0.01f;
    
    [SerializeField] private LayerMask _groundMask;
    
    public bool CheckGrounded()
    {
        float distance = MarginFloat;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, distance, _groundMask);

        return (hit.collider != null);
    }
}
